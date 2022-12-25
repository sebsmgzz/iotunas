namespace IoTunas.Extensions.Connectivity.Hosting;

using IoTunas.Core.DependencyInjection;
using IoTunas.Extensions.Connectivity.Hosting.Connectivity;

public static class IoTBuilderExtensions
{

    public static void UseConnectionObservers(
        this IIoTBuilder builder,
        Action<IConnectivityServiceBuilder>? configureAction = null)
    {
        builder.Services.AddConnectionObservers(configureAction);
    }

}
