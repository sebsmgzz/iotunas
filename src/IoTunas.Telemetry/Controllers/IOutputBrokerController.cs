namespace IoTunas.Extensions.Telemetry.Controllers;

using System;

public interface IOutputBrokerController
{

    public Type BrokerType { get; set; }

    TimeSpan Period { get; set; }

    bool Enabled { get; }

    void Start(bool immediately = false);

    void Stop(bool force = false);

}
