namespace IoTunas.Extensions.Telemetry.Hosting;

using IoTunas.Extensions.Telemetry.Collections;

public interface ITelemetryServiceBuilder
{

    IMetaProviderCollection Providers { get; }

    IMetaReceiverCollection Receivers { get; }

}
