namespace IoTunas.Extensions.Connectivity.Hosting;

using IoTunas.Core.DependencyInjection;

public static class IoTBuilderExtensions
{

    public static void UseConnectionObservers(
        this IIoTBuilder builder,
        Action<IConnectivityServiceBuilder>? configureAction = null)
    {
        builder.Services.AddConnectionObservers(configureAction);
    }

}
