namespace IoTunas.Connectivity.Builders;

using IoTunas.Connectivity.Collections;
using IoTunas.Connectivity.Factories;
using IoTunas.Connectivity.Mediators;
using Microsoft.Extensions.DependencyInjection;
using System;

public class ConnectionServicesBuilder : IConnectionServicesBuilder
{

    private readonly IServiceCollection services;

    public ConnectionServicesBuilder(IServiceCollection services)
    {
        this.services = services;
    }

    public void AddObservers(Action<IConnectionObserversMapping> configure)
    {
        var mapping = new ConnectionObserversMapping();
        configure?.Invoke(mapping);
        var mappingList = mapping.AsReadOnlyList();
        foreach (var map in mappingList)
        {
            services.AddTransient(map);
        }
        services.AddSingleton<IConnectionMediator, ConnectionMediator>();
        services.AddSingleton<IConnectionObserverFactory, ConnectionObserverFactory>(
            provider => new ConnectionObserverFactory(provider, mappingList));
    }

}
