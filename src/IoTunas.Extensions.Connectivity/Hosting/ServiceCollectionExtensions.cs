namespace IoTunas.Extensions.Connectivity.Hosting;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddConnectivityServices(
        this IServiceCollection services,
        Action<IConnectivityServiceBuilder>? configureAction = null)
    {
        var builder = new ConnectivityServiceBuilder();
        configureAction?.Invoke(builder);
        builder.AddObservableServices(services);
        return services;
    }

    public static IServiceCollection AddConnectionObserversOnly(
        this IServiceCollection services,
        Action<IConnectivityServiceBuilder>? configureAction = null)
    {
        var builder = new ConnectivityServiceBuilder();
        configureAction?.Invoke(builder);
        builder.AddObservableServices(services);
        return services;
    }

}
