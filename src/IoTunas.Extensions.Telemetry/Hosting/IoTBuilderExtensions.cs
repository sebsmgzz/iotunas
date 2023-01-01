namespace IoTunas.Extensions.Telemetry.Hosting;

using IoTunas.Core.DependencyInjection;

public static class IoTBuilderExtensions
{

    public static void UseTelemetryServices(
        this IIoTBuilder iotBuilder,
        Action<ITelemetryServiceBuilder>? configureAction = null)
    {
        iotBuilder.Services.AddTelemetryServices(configureAction);
    }

    public static void MapTelemetryServices(this IIoTBuilder iotBuilder)
    {
        iotBuilder.UseTelemetryServices(builder =>
        {
            builder.Receivers.Map();
            builder.Providers.Map();
        });
    }

    public static void MapTelemetryReceivers(this IIoTBuilder iotBuilder)
    {
        iotBuilder.Services.AddTelemetryReceptionOnly(builder => builder.Receivers.Map());
    }

    public static void MapTelemetryEmissions(this IIoTBuilder iotBuilder)
    {
        iotBuilder.Services.AddTelemetryEmissionOnly(builder => builder.Providers.Map());
    }

}
