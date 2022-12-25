namespace IoTunas.Extensions.Connectivity.Hosting;

using IoTunas.Extensions.Connectivity.Collections;
using IoTunas.Extensions.Connectivity.Factories;
using IoTunas.Extensions.Connectivity.Mediators;
using Microsoft.Extensions.DependencyInjection;

public class ConnectivityServiceBuilder : IConnectivityServiceBuilder
{

    public ConnectionObserverServiceCollection Observers { get; }

    public ConnectivityServiceBuilder()
    {
        Observers = new ConnectionObserverServiceCollection();
    }

    public void Build(IServiceCollection services)
    {
        var listing = new List<Type>();
        foreach (var descriptor in Observers)
        {
            services.AddScoped(descriptor.Type);
            listing.Add(descriptor.Type);
        }
        services.AddHostedService<ConnectivityHost>();
        services.AddSingleton<IConnectivityMediator, ConnectivityMediator>();
        services.AddSingleton<IConnectionObserverFactory>(p => new ConnectionObserverFactory(listing, p));
    }

}
