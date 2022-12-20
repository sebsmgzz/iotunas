namespace IoTunas.Extensions.Connectivity.Hosting;
using IoTunas.Extensions.Connectivity.Mediators;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;

public class DeviceConnectivityService : IHostedService
{

    private readonly IServiceProvider provider;

    public DeviceConnectivityService(IServiceProvider serviceProvider)
    {
        this.provider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var client = provider.GetRequiredService<DeviceClient>();
        var mediator = provider.GetRequiredService<IConnectivityMediator>();
        client.SetConnectionStatusChangesHandler(mediator.HandleConnectionChange);
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var client = provider.GetRequiredService<DeviceClient>();
        client.SetConnectionStatusChangesHandler(null);
        await Task.CompletedTask;
    }

}
