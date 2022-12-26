namespace IoTunas.Extensions.Connectivity.Hosting;

using IoTunas.Core.DependencyInjection;
using IoTunas.Extensions.Connectivity.Hosting.Connectivity;

public static class IoTBuilderExtensions
{

    public static void UseConnectionObservers(this IIoTBuilder builder)
    {
        builder.Services.AddConnectionObservers();
    }
    public static void UseConnectionObservers(
        this IIoTBuilder builder,
        Action<IConnectivityServiceBuilder> configureAction)
    {
        builder.Services.AddConnectionObservers(configureAction);
    }

}
