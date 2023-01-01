﻿namespace IoTunas.Extensions.Connectivity.Observables;

using IoTunas.Core.Services.ClientHosts;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;

public class ObservableConnectionService : IHostedService
{

    private readonly IServiceProvider provider;

    public ObservableConnectionService(IServiceProvider provider)
    {
        this.provider = provider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var clientHost = provider.GetRequiredService<IIoTClientHost>();
        var mediator = provider.GetRequiredService<IConnectivityMediator>();
        if (clientHost.IsEdgeCapable)
        {
            var client = provider.GetRequiredService<ModuleClient>();
            client.SetConnectionStatusChangesHandler(mediator.HandleConnectionChange);
        }
        else
        {
            var client = provider.GetRequiredService<DeviceClient>();
            client.SetConnectionStatusChangesHandler(mediator.HandleConnectionChange);
        }
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        var clientHost = provider.GetRequiredService<IIoTClientHost>();
        if (clientHost.IsEdgeCapable)
        {
            var client = provider.GetRequiredService<ModuleClient>();
            client.SetConnectionStatusChangesHandler(null);
        }
        else
        {
            var client = provider.GetRequiredService<DeviceClient>();
            client.SetConnectionStatusChangesHandler(null);
        }
        return Task.CompletedTask;
    }

}
