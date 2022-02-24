namespace IoTunas.Telemetry.Builders;

using IoTunas.Telemetry.Receivers.Collections;
using System;

public interface ITelemetryServicesBuilder
{

    void AddInputBrokers(Action<IInputTelemetryBrokerMapping> configure);
    
    void AddOutputBrokers();

}