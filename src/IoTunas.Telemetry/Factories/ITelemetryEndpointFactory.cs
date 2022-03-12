namespace IoTunas.Extensions.Telemetry.Factories;

using IoTunas.Extensions.Telemetry.Models;

public interface ITelemetryEndpointFactory
{

    int Count { get; }

    bool Contains(string endpointName);

    bool TryGet(string endpointName, out IInputTelemetryBroker receiver);

}
