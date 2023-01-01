namespace IoTunas.Extensions.Telemetry.Emission;

using IoTunas.Extensions.Telemetry.Models.Emission;
using System;

public interface ITelemetryController<TTelemetry> where TTelemetry : ITelemetry
{

    TimeSpan Period { get; set; }

    bool Running { get; }

    void Start(bool force = false);

    void Stop(bool force = false);

}
