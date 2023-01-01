namespace IoTunas.Extensions.Connectivity.Hosting;

using IoTunas.Core.DependencyInjection;

public static class IoTBuilderExtensions
{

    public static void UseTelemetryServices(
        this IIoTBuilder iotBuilder,
        Action<IConnectivityServiceBuilder>? configureAction = null)
    {
        iotBuilder.Services.AddConnectivityServices(configureAction);
    }

    public static void MapConnectivityServices(this IIoTBuilder iotBuilder)
    {
        iotBuilder.UseTelemetryServices(builder =>
        {
            builder.Observers.Map();
        });
    }

    public static void MapConnectionObservers(this IIoTBuilder iotBuilder)
    {
        iotBuilder.Services.AddConnectionObserversOnly(builder => builder.Observers.Map());
    }

}
