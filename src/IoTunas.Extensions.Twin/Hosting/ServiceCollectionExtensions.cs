namespace IoTunas.Extensions.Twin.Hosting;

using IoTunas.Extensions.Twin.Hosting.DesiredTwin;
using IoTunas.Extensions.Twin.Hosting.ReportedTwin;
using IoTunas.Extensions.Twin.Models;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddDesiredTwin<TTwin>(
        this IServiceCollection services)
        where TTwin : class, IDesiredTwinModel
    {
        var builder = new DesiredTwinServiceBuilder<TTwin>();
        builder.Build(services);
        return services;
    }

    public static IServiceCollection AddReportedTwin<TTwin>(
        this IServiceCollection services)
        where TTwin : class, IReportedTwinModel
    {
        var builder = new ReportedTwinServiceBuilder<TTwin>();
        builder.Build(services);
        return services;
    }

}
