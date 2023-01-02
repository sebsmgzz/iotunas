namespace IoTunas.Extensions.Telemetry.Collections;

using IoTunas.Extensions.Telemetry.Models.Emission;
using IoTunas.Core.Collections;

public class MetaProviderCollection : MetaTypeCollection<MetaProvider>, IMetaProviderCollection
{

    public override MetaProvider? Get(Type type)
    {
        return items.FirstOrDefault(provider => provider?.Type.Equals(type) ?? false, null);
    }

    public override bool Add(Type type)
    {
        return Add(new MetaProvider(type));
    }

}
