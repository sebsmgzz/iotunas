namespace IoTunas.Extensions.Connectivity.Factories;

using IoTunas.Extensions.Connectivity.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

public class ConnectionObserverFactory : IConnectionObserverFactory
{

    private readonly IServiceProvider provider;
    
    public IReadOnlyList<Type> Mapping { get; }

    public ConnectionObserverFactory(
        IServiceProvider provider,
        IReadOnlyList<Type> mapping)
    {
        this.provider = provider;
        Mapping = mapping;
    }

    public bool TryGet(Type type, out IConnectionObserver observer)
    {
        // Try getting the observer from the DI
        var scope = provider.CreateScope();
        var service = scope.ServiceProvider.GetService(type);
        observer = service as IConnectionObserver;
        return observer != null;
    }

    public bool TryGet<T>(out IConnectionObserver observer)
        where T : IConnectionObserver
    {
        return TryGet(typeof(T), out observer);
    }

    public bool TryGet(int index, out IConnectionObserver observer)
    {
        if (index < Mapping.Count)
        {
            return TryGet(Mapping[index], out observer);
        }
        observer = null;
        return false;
    }

}
