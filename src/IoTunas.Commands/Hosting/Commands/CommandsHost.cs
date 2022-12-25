namespace IoTunas.Extensions.Commands.Hosting.Commands;

using IoTunas.Core.Services.ClientHosts;
using IoTunas.Extensions.Commands.Mediators;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

public class CommandsHost : IHostedService
{

    private readonly IServiceProvider provider;
    private readonly IIoTClientHost clientHost;

    public CommandsHost(
        IServiceProvider provider,
        IIoTClientHost clientHost)
    {
        this.provider = provider;
        this.clientHost = clientHost;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var mediator = provider.GetRequiredService<ICommandMediator>();
        if (clientHost.IsEdgeCapable)
        {
            var client = provider.GetRequiredService<DeviceClient>();
            await client.SetMethodDefaultHandlerAsync(mediator.HandleAsync, this, cancellationToken);
        }
        else
        {
            var client = provider.GetRequiredService<ModuleClient>();
            await client.SetMethodDefaultHandlerAsync(mediator.HandleAsync, this, cancellationToken);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (clientHost.IsEdgeCapable)
        {
            var client = provider.GetRequiredService<DeviceClient>();
            await client.SetMethodDefaultHandlerAsync(null, null, cancellationToken);
        }
        else
        {
            var client = provider.GetRequiredService<ModuleClient>();
            await client.SetMethodDefaultHandlerAsync(null, null, cancellationToken);
        }
    }

}
