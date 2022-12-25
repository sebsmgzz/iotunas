namespace IoTunas.Extensions.Telemetry.Controllers;

using IoTunas.Extensions.Telemetry.Models;
using Microsoft.Azure.Devices.Client;
using System;
using Microsoft.Extensions.Logging;
using IoTunas.Extensions.Telemetry.Factories;

public abstract class OutputBrokerController : IOutputBrokerController
{

    public const int DefaultPeriodMinutes = 1;
    public const string InvalidBrokerLog =
        "Broker for {outputName} needs to " +
        "implement {interfaceName} and be registered " +
        "in the service provider's DI to handle a direct method invocation.";

    private readonly IOutputBrokerFactory factory;
    private readonly ILogger logger;

    private readonly Timer timer;
    private Task sendMessageTask;
    private CancellationTokenSource cts;

    public Type BrokerType { get; set; }

    public string OutputName { get; set; }

    public TimeSpan Period { get; set; }

    public bool Enabled { get; private set; }

    public OutputBrokerController(
        IOutputBrokerFactory factory,
        ILogger<IOutputBrokerController> logger)
    {
        this.factory = factory;
        this.logger = logger;
        timer = new Timer(HandleTimerFinished);
        sendMessageTask = Task.CompletedTask;
        cts = new CancellationTokenSource();
        Period = Timeout.InfiniteTimeSpan;
        Enabled = false;
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
        Enabled = true;
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
        Enabled = false;
    }

    private void HandleTimerFinished(object? state)
    {

        // TODO: Log it

        // Update period
        timer.Change(Period, Period);

        // If the current task is taking too long, cancel it
        if (sendMessageTask != null && !sendMessageTask.IsCompleted)
        {
            // TODO: Log it
            cts.Cancel();
        }

        // Create and call new broker
        cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(25));
        if (factory.TryGetValue(BrokerType, out var broker))
        {
            var messages = broker.HandleAsync(cts.Token).Result;
            sendMessageTask = SendMessagesAsync(messages, cts.Token);
        }
        else
        {
            logger.LogCritical(InvalidBrokerLog, OutputName, nameof(IOutputBroker));
        }

    }

    protected abstract Task SendMessagesAsync(
        Message[] messages,
        CancellationToken cancellationToken);

}
