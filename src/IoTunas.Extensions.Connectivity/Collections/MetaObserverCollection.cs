namespace IoTunas.Extensions.Connectivity.Collections;

using IoTunas.Extensions.Connectivity.Models.Observables;
using IoTunas.Core.Collections;
using System;

public class MetaObserverCollection : MetaTypeCollection<MetaObserver>, IMetaObserverCollection
{

    public override MetaObserver? Get(Type type)
    {
        return items.FirstOrDefault(meta => meta?.Type.Equals(type) ?? false, null);
    }

    public override bool Add(Type type)
    {
        return Add(new MetaObserver(type));
    }

}
