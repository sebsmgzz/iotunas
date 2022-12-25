namespace IoTunas.Extensions.Telemetry.Controllers;

using IoTunas.Extensions.Telemetry.Models;
using System;

public interface IEmissaryController
{

    EmissaryDescriptor Descriptor { get; }

    TimeSpan Period { get; set; }

    bool Running { get; }

    void Start(bool immediately = false);

    void Stop(bool force = false);

}
