namespace IoTunas.Extensions.Connectivity;
using IoTunas.Extensions.Connectivity.Mediators;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

public class Startup : IHostedService
{

    private readonly ModuleClient moduleClient;
    private readonly IServiceProvider serviceProvider;

    public Startup(
        ModuleClient moduleClient,
        IServiceProvider serviceProvider)
    {
        this.moduleClient = moduleClient;
        this.serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var service = serviceProvider.GetService<IConnectionMediator>();
        if (service != null)
        {
            moduleClient.SetConnectionStatusChangesHandler(
                service.HandleConnectionChange);
        }
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

}
