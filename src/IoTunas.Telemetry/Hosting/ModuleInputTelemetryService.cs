namespace IoTunas.Extensions.Telemetry.Hosting;

using IoTunas.Extensions.Telemetry.Mediators;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

public class ModuleInputTelemetryService : IHostedService
{

    private readonly IServiceProvider provider;

    public ModuleInputTelemetryService(IServiceProvider provider)
    {
        this.provider = provider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var client = provider.GetRequiredService<ModuleClient>();
        var mediator = provider.GetRequiredService<IInputBrokerMediator>();
        await client.SetMessageHandlerAsync(mediator.HandleAsync, this, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var client = provider.GetRequiredService<ModuleClient>();
        await client.SetMessageHandlerAsync(null, null, cancellationToken);
    }

}
