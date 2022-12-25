namespace IoTunas.Extensions.Telemetry.Hosting;

using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

public class ModuleOutputTelemetryService : IHostedService
{

    private readonly IServiceProvider provider;

    public ModuleOutputTelemetryService(IServiceProvider provider)
    {
        this.provider = provider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // TODO
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        // TODO
    }

}
