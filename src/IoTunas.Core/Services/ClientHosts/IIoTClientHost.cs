namespace IoTunas.Core.Services.ClientHosts;

using Microsoft.Extensions.Hosting;

public interface IIoTClientHost : IHostedService
{
    
    public bool EdgeCapable { get; }

    CancellationToken ClientOpened { get; }
    
    CancellationToken ClientClosed { get; }

}
