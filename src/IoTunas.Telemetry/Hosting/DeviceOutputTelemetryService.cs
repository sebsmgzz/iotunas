namespace IoTunas.Extensions.Telemetry.Hosting;

using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

public class DeviceOutputTelemetryService : IHostedService
{

    private readonly IServiceProvider provider;

    public DeviceOutputTelemetryService(IServiceProvider provider)
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
