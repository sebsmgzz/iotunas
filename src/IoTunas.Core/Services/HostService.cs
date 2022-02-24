namespace IoTunas.Core.Services;

using IoTunas.Core.Building;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Responsible for turning on and off the module client.
/// </summary>
internal sealed class HostService : IHostedService, IDisposable
{

    private readonly Lazy<ModuleClient> client;

    /// <summary>
    /// The module client handled.
    /// </summary>
    public ModuleClient Client => client.Value;

    public HostService(IClientBuilder builder)
    {
        client = new Lazy<ModuleClient>(builder.Build);
    }

    /// <inheritdoc cref="IHostedService.StartAsync(CancellationToken)"/>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await client.Value.OpenAsync(cancellationToken);
    }

    /// <inheritdoc cref="IHostedService.StopAsync(CancellationToken)"/>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (client.IsValueCreated)
        {
            await client.Value.CloseAsync(cancellationToken);
        }
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    public void Dispose()
    {
        if (client.IsValueCreated)
        {
            client.Value.Dispose();
        }
    }

}
