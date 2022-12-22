﻿namespace IoTunas.Extensions.Connectivity.Hosting;

using IoTunas.Core.Builders.Containers;
using IoTunas.Extensions.Connectivity.Builders;
using IoTunas.Extensions.Connectivity.Factories;
using IoTunas.Extensions.Connectivity.Mediators;
using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{

    public static void UseConnectivityServices(
        this IoTDeviceBuilder device,
        Action<IConnectionObserverMapping>? configure)
    {
        device.Services.AddHostedService<DeviceConnectivityService>();
        device.Services.UseConnetivityServicesAdHoc(configure);
    }

    public static void UseConnectivityServices(
        this IoTModuleBuilder module,
        Action<IConnectionObserverMapping>? configure)
    {
        module.Services.AddHostedService<ModuleConnectivityService>();
        module.Services.UseConnetivityServicesAdHoc(configure);
    }

    private static void UseConnetivityServicesAdHoc(
        this IServiceCollection services,
        Action<IConnectionObserverMapping>? configure)
    {
        var mapping = new ConnectionObserverMapping();
        configure?.Invoke(mapping);
        services.AddSingleton(mapping);
        foreach(var item in mapping)
        {
            services.AddTransient(item.Value);
        }
        services.AddSingleton<IConnectivityMediator, ConnectivityMediator>();
        services.AddSingleton<IConnectionObserverFactory, ConnectionObserverFactory>();
    }

}