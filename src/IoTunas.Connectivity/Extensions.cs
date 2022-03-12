namespace IoTunas.Extensions.Connectivity;

using IoTunas.Core.Builders.Containers;
using IoTunas.Extensions.Connectivity.Builders;
using IoTunas.Extensions.Connectivity.Factories;
using IoTunas.Extensions.Connectivity.Mediators;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{

    public static void UseConnectivityServices(
        this IoTDeviceBuilder device,
        Action<IConnectionObserversListBuilder> configure)
    {
        device.Services.UseConnetivityServicesAdHoc(configure);
        device.AfterBuild(async provider =>
        {
            var client = provider.GetRequiredService<DeviceClient>();
            var mediator = provider.GetRequiredService<IConnectionMediator>();
            client.SetConnectionStatusChangesHandler(mediator.HandleConnectionChange);
            await Task.CompletedTask;
        });
    }

    public static void UseConnectivityServices(
        this IoTModuleBuilder module,
        Action<IConnectionObserversListBuilder> configure)
    {
        module.Services.UseConnetivityServicesAdHoc(configure);
        module.AfterBuild(async provider =>
        {
            var client = provider.GetRequiredService<DeviceClient>();
            var mediator = provider.GetRequiredService<IConnectionMediator>();
            client.SetConnectionStatusChangesHandler(mediator.HandleConnectionChange);
            await Task.CompletedTask;
        });
    }

    private static void UseConnetivityServicesAdHoc(
        this IServiceCollection services,
        Action<IConnectionObserversListBuilder> configure)
    {

        // Add observers
        var builder = new ConnectionObserversListBuilder();
        configure.Invoke(builder);
        var mappingList = builder.Build();
        foreach (var map in mappingList)
        {
            services.AddScoped(map);
        }

        // Add connectivity services
        services.AddSingleton<IConnectionMediator, ConnectionMediator>();
        services.AddSingleton<IConnectionObserverFactory, ConnectionObserverFactory>(
            provider => new ConnectionObserverFactory(provider, mappingList));

    }

}
