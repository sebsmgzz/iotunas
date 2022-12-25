namespace IoTunas.Core.Services.ClientHosts.Modules;

using IoTunas.Core.Services.ClientBuilders.Modules;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public class IoTModuleHost : IoTHostBase, IIoTModuleHost
{

    public const string IoTVariablePrefix = "IOTEDGE_";

    private readonly IServiceProvider provider;
    private readonly Lazy<ModuleClient> client;

    public bool IsEdgeCapable => true;

    public ModuleClient Client => client.Value;

    public IoTModuleHost(
        IServiceProvider provider,
        ILogger<IoTModuleHost> logger)
        : base(logger)
    {
        this.provider = provider;
        client = new Lazy<ModuleClient>(CreateClient);
    }

    private ModuleClient CreateClient()
    {
        var builder = provider.GetRequiredService<IModuleClientBuilder>();
        return builder.Build();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        const string log = "Module client connection opened.";
        await Client
            .OpenAsync(cancellationToken)
            .ContinueWith(t => clientOpened.Value.Cancel(), cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        const string log = "Module client connection opened.";
        await Client
            .CloseAsync(cancellationToken)
            .ContinueWith(t => clientClosed.Value.Cancel(), cancellationToken);
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
