namespace IoTunas.Extensions.Connectivity.Mediators;

using IoTunas.Extensions.Connectivity.Factories;
using IoTunas.Extensions.Connectivity.Models;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;

public class ConnectionMediator : IConnectionMediator
{

    private readonly ModuleClient moduleClient;
    private readonly IServiceProvider serviceProvider;
    private readonly IConnectionObserverFactory factory;
    private readonly ILogger<IConnectionMediator> logger;

    public ConnectionMediator(
        ModuleClient moduleClient,
        IServiceProvider serviceProvider,
        IConnectionObserverFactory factory,
        ILogger<IConnectionMediator> logger)
    {
        this.moduleClient = moduleClient;
        this.serviceProvider = serviceProvider;
        this.factory = factory;
        this.logger = logger;
    }

    public void HandleConnectionChange(ConnectionStatus status, ConnectionStatusChangeReason reason)
    {
        HandleConnectionChangeAsync(status, reason).Wait();
    }

    public async Task HandleConnectionChangeAsync(ConnectionStatus status, ConnectionStatusChangeReason reason)
    {
        logger.LogInformation($"Change | {status} {reason}");
        var tasks = new List<Task>();
        foreach (var type in factory.Mapping)
        {
            // Try get observer, if failed, skip it and continue
            if (!factory.TryGet(type, out var observer))
            {
                logger.LogCritical(
                    $"Connection observer {type.Name} needs to " +
                    $"inherit {nameof(IConnectionObserver)} and be registered " +
                    $"in the service provider's DI to be invoked.");
                continue;
            }
            // Run task and save it
            tasks.Add(Task.Factory.StartNew(async () =>
            {
                try
                {
                    await observer.HandleConnectionChangeAsync(new()
                    {
                        Status = status,
                        Reason = reason
                    });
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Error | {type.Name}");
                }
            }));
        }
        await Task.WhenAll(tasks)
            .ContinueWith(t => logger.LogInformation(
                $"Observed | {tasks.Count} out of {factory.Mapping.Count}"));
    }

}
