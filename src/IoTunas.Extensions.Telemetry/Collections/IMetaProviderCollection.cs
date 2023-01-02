namespace IoTunas.Extensions.Telemetry.Collections;

using IoTunas.Core.Collections;
using IoTunas.Extensions.Telemetry.Models.Emission;

public interface IMetaProviderCollection : 
    IReadOnlyMetaProviderCollection, 
    IMetaTypeCollection<MetaProvider>
{
}
