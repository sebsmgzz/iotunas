namespace IoTunas.Extensions.Telemetry.Emission;

using IoTunas.Extensions.Telemetry.Collections;
using IoTunas.Extensions.Telemetry.Models.Emission;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

public class TelemetryController<TTelemetry> : ITelemetryController<TTelemetry> where TTelemetry : ITelemetry
{

    public const string MetaControllerNotFoundLog = "MetaController Not Found | " +
        "Meta controller of type {metaControllerType}, was not found in the meta controllers collection." +
        "Stopping the current telemetry loop.";
    public const string TelemetryLoopStarted = "Started | " +
        "Telemetry upload loop for {telemetry} with a period of {period}";
    public const string TelemetryLoopTookTooLongLog =
        "Telemetry upload loop for {telemetryType} is taking too long " +
        "or the emission period of {period} is too short. " +
        "Cancelling the current telemetry loop.";

    private readonly IReadOnlyMetaControllerCollection metaControllers;
    private readonly ITelemetrySender telemetrySender;
    private readonly ILogger logger;

    private MetaController? metaController;
    private readonly Timer timer;
    private Task sendTelemetryTask;
    private CancellationTokenSource cts;

    public TimeSpan Period { get; set; }

    public bool Running { get; private set; }

    public TelemetryController(
        IReadOnlyMetaControllerCollection metaControllers,
        ITelemetrySender telemetrySender,
        ILogger<ITelemetryController<TTelemetry>> logger)
    {
        this.metaControllers = metaControllers;
        this.telemetrySender = telemetrySender;
        this.logger = logger;
        metaController = null;
        timer = new Timer(HandleTimerFinished);
        sendTelemetryTask = Task.CompletedTask;
        cts = new CancellationTokenSource();
        Period = Timeout.InfiniteTimeSpan;
        Running = false;
    }

    /// <summary>
    /// Starts the telemetry upload loop.
    /// </summary>
    /// <param name="force">
    /// Starts the telemetry upload loop immediately, 
    /// else it awaits the period timespan before actually starting.
    /// </param>
    public void Start(bool force = false)
    {
        var dueTime = force ? TimeSpan.Zero : Period;
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
            cts?.Cancel();
        }
        Running = false;
    }

    private void HandleTimerFinished(object? state)
    {

        // Log loop start and update period
        timer.Change(Period, Period);
        var telemetryType = typeof(TTelemetry);
        logger.LogInformation(TelemetryLoopStarted, telemetryType.Name, Period);

        // If the current task is taking too long, cancel it
        if (sendTelemetryTask != null && cts != null && !sendTelemetryTask.IsCompleted)
        {
            logger.LogWarning(TelemetryLoopTookTooLongLog, telemetryType.Name, Period);
            cts.Cancel();
        }

        // Get the meta controller if required
        if (metaController == null && !metaControllers.TryGet(
            type: GetType(),
            controller: out metaController))
        {
            logger.LogCritical(MetaControllerNotFoundLog, GetType());
            Stop();
            return;
        }

        // Start a new telemetry upload loop
        cts = new CancellationTokenSource();
        sendTelemetryTask = telemetrySender.SendAsync(metaController.Provider.Type, null, cts.Token);

    }

}
