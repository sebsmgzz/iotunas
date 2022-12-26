namespace IoTunas.Extensions.Twin.Hosting;

using IoTunas.Core.DependencyInjection;
using IoTunas.Extensions.Twin.Models;

public static class IoTBuilderExtensions
{

    public static void UseDesiredTwin<TTwin>(
        this IIoTBuilder builder)
        where TTwin : class, IDesiredTwinModel
    {
        builder.Services.AddDesiredTwin<TTwin>();
    }

    public static void UseReportedTwin<TTwin>(
        this IIoTBuilder builder)
        where TTwin : class, IReportedTwinModel
    {
        builder.Services.AddReportedTwin<TTwin>();
    }

}
