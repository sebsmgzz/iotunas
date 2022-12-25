namespace IoTunas.Core.Services.ClientHosts;

using Microsoft.Extensions.Hosting;

public interface IIoTClientHost : IHostedService
{
    
    public bool IsEdgeCapable { get; }

    CancellationToken ClientOpened { get; }
    
    CancellationToken ClientClosed { get; }

}
