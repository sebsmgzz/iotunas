namespace IoTunas.Connectivity;

using IoTunas.Connectivity.Builders;
using IoTunas.Connectivity.Factories;
using IoTunas.Connectivity.Mediators;
using IoTunas.Core.Builders;
using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{

    public static void UseConnectivityServices(
        this IIoTBuilder iotBuilder,
        Action<IConnectionObserversListBuilder> configure)
    {
        iotBuilder.ConfigureServices(services =>
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

        });
    }

}
