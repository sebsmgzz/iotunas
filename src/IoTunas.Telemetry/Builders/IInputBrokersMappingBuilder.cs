namespace IoTunas.Telemetry.Builders;

using IoTunas.Telemetry.Models;

public interface IInputBrokersMappingBuilder
{

    void AddReceiver<T>(string inputName) where T : IInputTelemetryBroker;

    void AddReceiver<T>() where T : IInputTelemetryBroker;

    void MapReceivers();
    
    IReadOnlyDictionary<string, Type> Build();

}
