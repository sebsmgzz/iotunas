namespace IoTunas.Core.Services;

using IoTunas.Core.Builders.ModuleClients;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

internal class ModuleHostService : IHostedService, IDisposable
{

    private readonly Lazy<ModuleClient> moduleClient;
    private readonly ILogger logger;

    public ModuleClient Client => moduleClient.Value;

    public ModuleHostService(
        IModuleClientBuilder builder,
        ILogger<DeviceHostService> logger)
    {
        this.logger = logger;
        moduleClient = new Lazy<ModuleClient>(builder.Build);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await moduleClient.Value
            .OpenAsync(cancellationToken)
            .ContinueWith(t =>
                logger.LogInformation("Module client connection opened."),
                cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (moduleClient.IsValueCreated)
        {
            await moduleClient.Value
                .CloseAsync(cancellationToken)
                .ContinueWith(t =>
                    logger.LogInformation("Module client connection closed."),
                    cancellationToken);
        }
    }

    public void Dispose()
    {
        if (moduleClient.IsValueCreated)
        {
            moduleClient.Value.Dispose();
        }
    }

}
