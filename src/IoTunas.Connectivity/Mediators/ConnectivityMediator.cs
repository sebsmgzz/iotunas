namespace IoTunas.Extensions.Connectivity.Mediators;

using IoTunas.Extensions.Connectivity.Factories;
using IoTunas.Extensions.Connectivity.Models;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;

public class ConnectivityMediator : IConnectivityMediator
{

    public const string ObserverdLog = "Observed | {status} {reason}";

    private readonly IConnectionObserverFactory factory;
    private readonly ILogger<IConnectivityMediator> logger;

    public ConnectivityMediator(
        IConnectionObserverFactory factory,
        ILogger<IConnectivityMediator> logger)
    {
        this.factory = factory;
        this.logger = logger;
    }

    public void HandleConnectionChange(
        ConnectionStatus status, ConnectionStatusChangeReason reason)
    {
        HandleConnectionChangeAsync(status, reason).Wait();
    }

    public async Task HandleConnectionChangeAsync(
        ConnectionStatus status, ConnectionStatusChangeReason reason)
    {
        logger.LogInformation(ObserverdLog, status, reason);
        var tasks = new List<Task>();
        var args = new ConnectionChangeArgs(status, reason);
        foreach (var observer in factory.GetAll())
        {
            var task = observer.HandleConnectionChangeAsync(args);
            tasks.Add(task);
        }
        await Task.WhenAll(tasks);
    }

}
