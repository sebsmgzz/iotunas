namespace IoTunas.Extensions.Telemetry.Hosting.Reception;

using IoTunas.Extensions.Telemetry.Collections;

public interface IReceptionServiceBuilder
{
    
    ReceiverServiceCollection Receivers { get; }

}
