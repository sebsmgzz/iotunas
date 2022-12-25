namespace IoTunas.Extensions.Telemetry.Hosting;

using IoTunas.Extensions.Telemetry.Mediators;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

public class DeviceInputTelemetryService : IHostedService
{

    private readonly IServiceProvider provider;

    public DeviceInputTelemetryService(IServiceProvider provider)
    {
        this.provider = provider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var client = provider.GetRequiredService<DeviceClient>();
        var mediator = provider.GetRequiredService<IInputBrokerMediator>();
        await client.SetReceiveMessageHandlerAsync(mediator.HandleAsync, this, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var client = provider.GetRequiredService<DeviceClient>();
        await client.SetReceiveMessageHandlerAsync(null, null, cancellationToken);
    }

}
