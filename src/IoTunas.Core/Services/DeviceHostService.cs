namespace IoTunas.Core.Services;

using IoTunas.Core.Builders.DeviceClients;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

internal class DeviceHostService : IHostedService, IDisposable
{

    private readonly Lazy<DeviceClient> deviceClient;
    private readonly ILogger logger;

    public DeviceClient Client => deviceClient.Value;

    public DeviceHostService(
        IDeviceClientBuilder builder,
        ILogger<DeviceHostService> logger)
    {
        this.logger = logger;
        deviceClient = new Lazy<DeviceClient>(builder.Build);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await deviceClient.Value
            .OpenAsync(cancellationToken)
            .ContinueWith(t =>
                logger.LogInformation("Device client connection opened."),
                cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (!deviceClient.IsValueCreated)
        {
            await deviceClient.Value
                .CloseAsync(cancellationToken)
                .ContinueWith(t =>
                    logger.LogInformation("Device client connection closed."),
                    cancellationToken);
        }
    }

    public void Dispose()
    {
        if (!deviceClient.IsValueCreated)
        {
            deviceClient.Value.Dispose();
        }
    }

}
