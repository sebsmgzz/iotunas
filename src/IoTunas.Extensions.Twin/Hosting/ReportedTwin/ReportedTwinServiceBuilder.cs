namespace IoTunas.Extensions.Twin.Hosting.ReportedTwin;

using IoTunas.Extensions.Twin.Models;
using IoTunas.Extensions.Twin.Services.Mediators;
using Microsoft.Extensions.DependencyInjection;

public class ReportedTwinServiceBuilder<TTwin> where TTwin : class, IReportedTwinModel
{

    public TTwin? InitialTwin { get; set; }

    public ReportedTwinServiceBuilder()
    {
    }

    public void Build(IServiceCollection services)
    {
        if (InitialTwin == null)
        {
            services.AddSingleton<TTwin>();
        }
        else
        {
            services.AddSingleton<TTwin>(InitialTwin);
        }
        services.AddSingleton<IReportedTwinModel>(p => p.GetRequiredService<TTwin>());
        services.AddSingleton<IReportedTwinMediator, ReportedTwinMediator>();
    }

}
