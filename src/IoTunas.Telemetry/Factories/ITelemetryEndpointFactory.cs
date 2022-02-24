namespace IoTunas.Telemetry.Factories;

using IoTunas.Telemetry.Models;

public interface ITelemetryEndpointFactory
{

    int Count { get; }

    bool Contains(string endpointName);

    bool TryGet(string endpointName, out IInputTelemetryBroker receiver);

}
