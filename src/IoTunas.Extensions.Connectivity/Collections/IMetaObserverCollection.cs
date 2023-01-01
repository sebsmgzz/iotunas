namespace IoTunas.Extensions.Connectivity.Collections;

using IoTunas.Extensions.Connectivity.Models.Observables;
using System;
using System.Reflection;

public interface IMetaObserverCollection : IReadOnlyMetaObserverCollection
{

    bool Add(MetaObserver observer);

    bool Add(Type type);

    bool Add<TType>() where TType : IConnectionObserver;

    bool Remove(MetaObserver observer);

    bool Remove(Type type);

    bool Remove<TType>() where TType : IConnectionObserver;

    void Map(Assembly assembly);

    void Map();

}
