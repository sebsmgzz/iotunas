namespace IoTunas.Extensions.Telemetry.Hosting;

using IoTunas.Core.DependencyInjection;
using IoTunas.Extensions.Telemetry.Hosting.Emission;
using IoTunas.Extensions.Telemetry.Hosting.Reception;

public static class IoTBuilderExtensions
{

    public static void UseTelemetryReception(this IIoTBuilder builder)
    {
        builder.Services.AddTelemetryReceivers();
    }

    public static void UseTelemetryReception(
        this IIoTBuilder builder,
        Action<IReceptionServiceBuilder> configureAction)
    {
        builder.Services.AddTelemetryReceivers(configureAction);
    }

    public static void UseTelemetryEmission(this IIoTBuilder builder)
    {
        builder.Services.AddTelemetryEmissaries();
    }

    public static void UseTelemetryEmission(
        this IIoTBuilder builder,
        Action<IEmissionServiceBuilder> configureAction)
    {
        builder.Services.AddTelemetryEmissaries(configureAction);
    }

}
