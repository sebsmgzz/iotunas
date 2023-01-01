namespace IoTunas.Extensions.Telemetry.Hosting;

using IoTunas.Core.DependencyInjection;

public static class IoTBuilderExtensions
{

    public static void UseTelemetryServices(
        this IIoTBuilder builder,
        Action<ITelemetryServiceBuilder>? configureAction = null)
    {
        builder.Services.AddTelemetryServices(configureAction);
    }

    public static void MapTelemetryServices(this IIoTBuilder builder)
    {
        builder.Services.AddTelemetryServices(builder =>
        {
            builder.Receivers.Map();
            builder.Providers.Map();
        });
    }

    public static void MapTelemetryReceivers(this IIoTBuilder builder)
    {
        builder.Services.AddTelemetryReceptionOnly(builder => builder.Receivers.Map());
    }

    public static void MapTelemetryEmissions(this IIoTBuilder builder)
    {
        builder.Services.AddTelemetryEmissionOnly(builder => builder.Providers.Map());
    }

}
