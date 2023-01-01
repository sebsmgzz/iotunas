namespace IoTunas.Extensions.Connectivity.Hosting;

using IoTunas.Extensions.Connectivity.Collections;
using IoTunas.Extensions.Connectivity.Observable;
using Microsoft.Extensions.DependencyInjection;

public class ConnectivityServiceBuilder : IConnectivityServiceBuilder
{

    public IMetaObserverCollection Observers { get; }

    public ConnectivityServiceBuilder()
    {
        Observers = new MetaObserverCollection();
    }

    public void AddObservableServices(IServiceCollection services)
    {
        foreach (var observer in Observers)
        {
            services.AddScoped(observer.Type);
        }
        services.AddHostedService<ObservableConnectionService>();
        services.AddSingleton<IReadOnlyMetaObserverCollection>(Observers);
        services.AddSingleton<IConnectivityMediator, ConnectivityMediator>();
        services.AddSingleton<IConnectionObserverFactory, ConnectionObserverFactory>();
    }

}
