namespace IoTunas.Telemetry.Receivers.Collections;

using IoTunas.Telemetry.Models;

public interface IInputTelemetryBrokerMapping
{

    void AddReceiver<T>(string inputName) where T : IInputTelemetryBroker;

    void AddReceiver<T>() where T : IInputTelemetryBroker;

    void MapReceivers();
    
    IReadOnlyDictionary<string, Type> AsReadOnlyDictionary();

}
