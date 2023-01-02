namespace IoTunas.Extensions.Telemetry.Collections;

using IoTunas.Core.Collections;
using IoTunas.Extensions.Telemetry.Models.Emission;

public interface IReadOnlyMetaProviderCollection : 
    IReadOnlyMetaTypeCollection<MetaProvider>, 
    IEnumerable<MetaProvider>
{
}
