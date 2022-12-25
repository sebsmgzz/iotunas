namespace IoTunas.Extensions.Connectivity.Hosting;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddConnectionObservers(
        this IServiceCollection services,
        Action<IConnectivityServiceBuilder>? configureAction = null)
    {
        var builder = new ConnectivityServiceBuilder();
        configureAction?.Invoke(builder);
        builder.Build(services);
        return services;
    }

}
