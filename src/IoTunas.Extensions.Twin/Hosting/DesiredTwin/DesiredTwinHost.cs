namespace IoTunas.Extensions.Twin.Hosting.DesiredTwin;

using IoTunas.Core.Services.ClientHosts;
using IoTunas.Extensions.Twin.Services.Mediators;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

public class DesiredTwinHost : IHostedService
{

    private readonly IServiceProvider provider;

    public DesiredTwinHost(IServiceProvider provider)
    {
        this.provider = provider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var clientHost = provider.GetRequiredService<IIoTClientHost>();
        var mediator = provider.GetRequiredService<IDesiredTwinMediator>();
        if (clientHost.IsEdgeCapable)
        {
            var client = provider.GetRequiredService<ModuleClient>();
            await client.SetDesiredPropertyUpdateCallbackAsync(
                mediator.HandlePropertyUpdate, this, cancellationToken);
        }
        else
        {
            var client = provider.GetRequiredService<DeviceClient>();
            await client.SetDesiredPropertyUpdateCallbackAsync(
                mediator.HandlePropertyUpdate, this, cancellationToken);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var clientHost = provider.GetRequiredService<IIoTClientHost>();
        if (clientHost.IsEdgeCapable)
        {
            var client = provider.GetRequiredService<ModuleClient>();
            await client.SetDesiredPropertyUpdateCallbackAsync(null, null, cancellationToken);
        }
        else
        {
            var client = provider.GetRequiredService<DeviceClient>();
            await client.SetDesiredPropertyUpdateCallbackAsync(null, null, cancellationToken);
        }
    }

}
