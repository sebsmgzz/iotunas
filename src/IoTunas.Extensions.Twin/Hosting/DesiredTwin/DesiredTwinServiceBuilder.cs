namespace IoTunas.Extensions.Twin.Hosting.DesiredTwin;

using IoTunas.Extensions.Twin.Models;
using IoTunas.Extensions.Twin.Services.Mediators;
using Microsoft.Extensions.DependencyInjection;

public class DesiredTwinServiceBuilder<TTwin> where TTwin : class, IDesiredTwinModel
{

    public TTwin? InitialTwin { get; set; }

    public DesiredTwinServiceBuilder()
    {
    }

    public void Build(IServiceCollection services)
    {
        if(InitialTwin == null)
        {
            services.AddSingleton<TTwin>();
        }
        else
        {
            services.AddSingleton<TTwin>(InitialTwin);
        }
        services.AddSingleton<IDesiredTwinModel>(p => p.GetRequiredService<TTwin>());
        services.AddSingleton<IDesiredTwinMediator, DesiredTwinMediator>();
    }

}
