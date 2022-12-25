namespace IoTunas.Core.Hosting;

using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

internal class IoTModuleHostedService : IHostedService, IDisposable, IAsyncDisposable
{

    private readonly ModuleClient client;
    private readonly ILogger logger;

    public IoTModuleHostedService(
        ModuleClient client,
        ILogger<IoTModuleHostedService> logger)
    {
        this.client = client;
        this.logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        const string log = "Module client connection opened.";
        await client
            .OpenAsync(cancellationToken)
            .ContinueWith(t => logger.LogInformation(log), cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        const string log = "Module client connection opened.";
        await client
            .CloseAsync(cancellationToken)
            .ContinueWith(t => logger.LogInformation(log), cancellationToken);
    }

    public void Dispose()
    {
        client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return client.DisposeAsync();
    }

}
