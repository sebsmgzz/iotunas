namespace IoTunas.Core.Services.ClientHosts.Devices;

using IoTunas.Core.Services.ClientBuilders.Devices;
using IoTunas.Core.Services.ClientHosts;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

public class IoTDeviceHost : IoTHostBase, IIoTDeviceHost
{

    public const string IoTVariablePrefix = "IOT_";

    private readonly IServiceProvider provider;
    private readonly Lazy<DeviceClient> client;

    public bool EdgeCapable => false;

    public DeviceClient Client => client.Value;

    public IoTDeviceHost(
        IServiceProvider provider,
        ILogger<IoTDeviceHost> logger)
        : base(logger)
    {
        this.provider = provider;
        client = new Lazy<DeviceClient>(CreateClient);
    }

    private DeviceClient CreateClient()
    {
        var builder = provider.GetRequiredService<IDeviceClientBuilder>();
        return builder.Build();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Client
            .OpenAsync(cancellationToken)
            .ContinueWith(_ => clientOpened.Value.Cancel(), cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Client
            .CloseAsync(cancellationToken)
            .ContinueWith(_ => clientClosed.Value.Cancel(), cancellationToken);
    }

    public void Dispose()
    {
        Client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return Client.DisposeAsync();
    }

}
