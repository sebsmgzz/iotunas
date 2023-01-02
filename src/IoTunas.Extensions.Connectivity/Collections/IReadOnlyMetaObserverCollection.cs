namespace IoTunas.Extensions.Connectivity.Collections;

using IoTunas.Core.Collections;
using IoTunas.Extensions.Connectivity.Models.Observables;
using System.Collections.Generic;

public interface IReadOnlyMetaObserverCollection :
    IReadOnlyMetaTypeCollection<MetaObserver>,
    IEnumerable<MetaObserver>
{
}
