namespace IoTunas.Extensions.Connectivity.Collections;

using IoTunas.Extensions.Connectivity.Models.Observables;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public interface IReadOnlyMetaObserverCollection : IEnumerable<MetaObserver>
{

    int Count { get; }

    MetaObserver? Get(Type type);

    bool TryGet(
        Type type, 
        [MaybeNullWhen(false)] out MetaObserver observer);

}
