namespace IoTunas.Extensions.Telemetry.Hosting;

using IoTunas.Extensions.Telemetry.Hosting.Emission;
using IoTunas.Extensions.Telemetry.Hosting.Reception;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddTelemetryReceivers(this IServiceCollection services)
    {
        return services.AddTelemetryReceivers(builder => builder.Receivers.Map());
    }

    public static IServiceCollection AddTelemetryReceivers(
        this IServiceCollection services,
        Action<IReceptionServiceBuilder> configureAction)
    {
        var builder = new ReceptionServiceBuilder();
        configureAction.Invoke(builder);
        builder.Build(services);
        return services;
    }
    
    public static IServiceCollection AddTelemetryEmissaries(this IServiceCollection services)
    {
        return services.AddTelemetryEmissaries(builder => builder.Emissaries.Map());
    }

    public static IServiceCollection AddTelemetryEmissaries(
        this IServiceCollection services,
        Action<IEmissionServiceBuilder> configureAction)
    {
        var builder = new EmissionServiceBuilder();
        configureAction?.Invoke(builder);
        builder.Build(services);
        return services;
    }

}
