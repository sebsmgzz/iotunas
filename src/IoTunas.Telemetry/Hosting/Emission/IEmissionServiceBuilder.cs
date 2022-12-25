namespace IoTunas.Extensions.Telemetry.Hosting.Emission;

using IoTunas.Extensions.Telemetry.Collections;

public interface IEmissionServiceBuilder
{

    EmissaryServiceCollection Emissaries { get; }

}
