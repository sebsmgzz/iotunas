namespace IoTunas.Extensions.Connectivity.Collections;

using IoTunas.Core.Collections;
using IoTunas.Extensions.Connectivity.Models.Observables;

public interface IMetaObserverCollection : 
    IMetaTypeCollection<MetaObserver>,
    IReadOnlyMetaObserverCollection
{
}
