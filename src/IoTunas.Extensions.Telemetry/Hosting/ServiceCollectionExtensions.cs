namespace IoTunas.Extensions.Telemetry.Hosting;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddTelemetryServices(
        this IServiceCollection services,
        Action<ITelemetryServiceBuilder>? configureAction = null)
    {
        var builder = new TelemetryServiceBuilder();
        configureAction?.Invoke(builder);
        builder.AddReceptionServices(services);
        builder.AddEmissionServices(services);
        return services;
    }

    public static IServiceCollection AddTelemetryReceptionOnly(
        this IServiceCollection services,
        Action<ITelemetryServiceBuilder>? configureAction = null)
    {
        var builder = new TelemetryServiceBuilder();
        configureAction?.Invoke(builder);
        builder.AddReceptionServices(services);
        return services;
    }
    
    public static IServiceCollection AddTelemetryEmissionOnly(
        this IServiceCollection services,
        Action<ITelemetryServiceBuilder>? configureAction = null)
    {
        var builder = new TelemetryServiceBuilder();
        configureAction?.Invoke(builder);
        builder.AddEmissionServices(services);
        return services;
    }

}
