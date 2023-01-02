namespace IoTunas.Demos.Template.Connectivity;

using IoTunas.Extensions.Connectivity.Models.Observables;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public class ConnectionObserver : IConnectionObserver
{

    public const string ConnectionChangeLog = "Connection changed: Status: {status} Reason: {reason}";
    private readonly ILogger logger;

    public ConnectionObserver(ILogger<IConnectionObserver> logger)
    {
        this.logger = logger;
    }

    public Task HandleConnectionChangeAsync(ConnectionChangeArgs args)
    {
        logger.LogWarning(ConnectionChangeLog, args.Status, args.Reason);
        return Task.CompletedTask;
    }

}
