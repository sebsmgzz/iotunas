namespace IoTunas.Extensions.Telemetry.Controllers;

using Microsoft.Azure.Devices.Client;
using System;
using Microsoft.Extensions.Logging;
using IoTunas.Extensions.Telemetry.Factories;
using Microsoft.Extensions.DependencyInjection;
using IoTunas.Core.Services.ClientHosts;
using IoTunas.Extensions.Telemetry.Models;

public class EmissaryController : IEmissaryController
{

    public const string EmissionStarted = "Started | Emission {outputName}";
    public const string EmissaryNotFoundLog =
        "Emissary for {outputName} was not foun. " +
        "Stopping the telemetry emission loop.";
    public const string EmissaryTookTooLongLog =
        "Telemetry emission for {outputName} is taking too long " +
        "or the emission period of {period} is too short. " +
        "Cancelling the current emission.";

    private readonly IServiceProvider provider;
    private readonly IEmissaryFactory factory;
    private readonly IIoTClientHost clientHost;
    private readonly ILogger logger;

    private readonly EmissaryDescriptor descriptor;
    private readonly Timer timer;
    private Task sendMessageTask;
    private CancellationTokenSource cts;

    public EmissaryDescriptor Descriptor { get; }

    public TimeSpan Period { get; set; }

    public bool Running { get; private set; }

    public EmissaryController(
        EmissaryDescriptor descriptor, 
        IServiceProvider provider)
    {
        this.descriptor = descriptor;
        this.provider = provider;
        factory = provider.GetRequiredService<IEmissaryFactory>();
        clientHost = provider.GetRequiredService<IIoTClientHost>();
        logger = provider.GetRequiredService<ILogger<IEmissaryController>>();
        timer = new Timer(HandleTimerFinished);
        sendMessageTask = Task.CompletedTask;
        cts = new CancellationTokenSource();
        Period = descriptor.InitialPeriod;
        Running = false;
    }

    /// <summary>
    /// Starts the telemetry upload loop.
    /// </summary>
    /// <param name="immediately">
    /// Starts the telemetry upload loop immediately, 
    /// else it awaits the period timespan before actually starting.
    /// </param>
    public void Start(bool immediately = false)
    {
        var dueTime = immediately ? TimeSpan.Zero : Period;
        timer.Change(dueTime, Period);
        Running = true;
    }

    /// <summary>
    /// Stops the telemetry upload loop.
    /// </summary>
    /// <param name="force">
    /// Forces the current upload task to be cancelled, 
    /// else the last task is allowed to finish.
    /// </param>
    public void Stop(bool force = false)
    {
        timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
        if (force)
        {
            cts.Cancel();
        }
        Running = false;
    }

    private void HandleTimerFinished(object? state)
    {

        // Update period
        logger.LogInformation(EmissionStarted, descriptor.OutputName);
        timer.Change(Period, Period);

        // If the current task is taking too long, cancel it
        if (sendMessageTask != null && !sendMessageTask.IsCompleted)
        {
            logger.LogWarning(EmissaryTookTooLongLog, descriptor.OutputName, Period);
            cts.Cancel();
        }

        // Create and call new broker
        if (factory.TryGetValue(descriptor.Type, out var broker))
        {
            cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(25));
            var messages = broker.HandleAsync(cts.Token).Result;
            sendMessageTask = clientHost.IsEdgeCapable ?
                SendModuleMessagesAsync(messages, cts.Token) :
                SendDeviceMessagesAsync(messages, cts.Token);
        }
        else
        {
            logger.LogCritical(EmissaryNotFoundLog, descriptor.OutputName);
            Stop(force: true);
        }

    }

    private Task SendModuleMessagesAsync(
        Message[] messages,
        CancellationToken cancellationToken)
    {
        var client = provider.GetRequiredService<ModuleClient>();
        if (messages.Length == 1)
        {
            return client.SendEventAsync(descriptor.OutputName, messages[0], cancellationToken);
        }
        else
        {
            return client.SendEventBatchAsync(descriptor.OutputName, messages, cancellationToken);
        }
    }

    private Task SendDeviceMessagesAsync(
        Message[] messages,
        CancellationToken cancellationToken)
    {
        var client = provider.GetRequiredService<DeviceClient>();
        if (messages.Length == 1)
        {
            return client.SendEventAsync(messages[0], cancellationToken);
        }
        else
        {
            return client.SendEventBatchAsync(messages, cancellationToken);
        }
    }

}
