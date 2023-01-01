﻿namespace IoTunas.Extensions.Telemetry.Reception;

using IoTunas.Core.Services.ClientHosts;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

public class ReceptionService : IHostedService
{

    private readonly IServiceProvider provider;

    public ReceptionService(IServiceProvider provider)
    {
        this.provider = provider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var mediator = provider.GetRequiredService<IReceiverMediator>();
        var clientHost = provider.GetRequiredService<IIoTClientHost>();
        if (clientHost.IsEdgeCapable)
        {
            var client = provider.GetRequiredService<ModuleClient>();
            await client.SetMessageHandlerAsync(mediator.HandleAsync, this, cancellationToken);
        }
        else
        {
            var client = provider.GetRequiredService<DeviceClient>();
            await client.SetReceiveMessageHandlerAsync(mediator.HandleAsync, this, cancellationToken);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var clientHost = provider.GetRequiredService<IIoTClientHost>();
        if (clientHost.IsEdgeCapable)
        {
            var client = provider.GetRequiredService<ModuleClient>();
            await client.SetMessageHandlerAsync(null, null, cancellationToken);
        }
        else
        {
            var client = provider.GetRequiredService<DeviceClient>();
            await client.SetReceiveMessageHandlerAsync(null, null, cancellationToken);
        }
    }

}
