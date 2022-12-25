namespace IoTunas.Extensions.Telemetry.Hosting;

using IoTunas.Core.DependencyInjection;
using IoTunas.Extensions.Telemetry.Hosting.Emission;
using IoTunas.Extensions.Telemetry.Hosting.Reception;

public static class IoTBuilderExtensions
{

    public static void UseTelemetryReception(
        this IIoTBuilder builder,
        Action<IReceptionServiceBuilder>? configureAction = null)
    {
        builder.Services.AddTelemetryReceivers(configureAction);
    }

    public static void UseTelemetryEmission(
        this IIoTBuilder builder,
        Action<IEmissionServiceBuilder>? configureAction = null)
    {
        builder.Services.AddTelemetryEmissaries(configureAction);
    }

}
