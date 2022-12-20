namespace IoTunas.Extensions.Connectivity.Hosting;
using IoTunas.Extensions.Connectivity.Mediators;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;

public class ModuleConnectivityService : IHostedService
{

    private readonly IServiceProvider provider;

    public ModuleConnectivityService(IServiceProvider provider)
    {
        this.provider = provider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var client = provider.GetRequiredService<ModuleClient>();
        var mediator = provider.GetRequiredService<IConnectivityMediator>();
        client.SetConnectionStatusChangesHandler(mediator.HandleConnectionChange);
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var client = provider.GetRequiredService<ModuleClient>();
        client.SetConnectionStatusChangesHandler(null);
        await Task.CompletedTask;
    }

}
