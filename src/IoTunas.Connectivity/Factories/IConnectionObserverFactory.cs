namespace IoTunas.Extensions.Connectivity.Factories;

using IoTunas.Extensions.Connectivity.Models;
using System;

public interface IConnectionObserverFactory
{

    public IReadOnlyList<Type> Mapping { get; }

    bool TryGet(int index, out IConnectionObserver observer);
    
    bool TryGet(Type type, out IConnectionObserver observer);
    
    bool TryGet<T>(out IConnectionObserver observer) where T : IConnectionObserver;

}