namespace IoTunas.Extensions.Telemetry.Builders;

using IoTunas.Extensions.Telemetry.Models;

public interface IInputBrokersMappingBuilder
{

    void AddReceiver<T>(string inputName) where T : IInputTelemetryBroker;

    void AddReceiver<T>() where T : IInputTelemetryBroker;

    void MapReceivers();
    
    IReadOnlyDictionary<string, Type> Build();

}
