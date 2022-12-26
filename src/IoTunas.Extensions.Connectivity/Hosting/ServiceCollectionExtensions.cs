namespace IoTunas.Extensions.Connectivity.Hosting;

using IoTunas.Extensions.Connectivity.Hosting.Connectivity;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConnectionObservers(this IServiceCollection services)
    {
        return services.AddConnectionObservers(builder => builder.Observers.Map());
    }

    public static IServiceCollection AddConnectionObservers(
        this IServiceCollection services,
        Action<IConnectivityServiceBuilder> configureAction)
    {
        var builder = new ConnectivityServiceBuilder();
        configureAction?.Invoke(builder);
        builder.Build(services);
        return services;
    }

}
