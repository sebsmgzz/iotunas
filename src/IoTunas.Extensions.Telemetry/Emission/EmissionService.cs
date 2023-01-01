namespace IoTunas.Extensions.Telemetry.Emission;

using IoTunas.Core.DependencyInjection;
using IoTunas.Extensions.Telemetry.Collections;
using IoTunas.Extensions.Telemetry.Models.Emission;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

public class EmissionService : IHostedService
{

    public const string ControllerServiceNotFoundLog = "Controller Not Found | " +
        "The controller of type {controllerType} was not found in the service provider. " +
        "Please, ensure the provider has a telemetry loop attached to it, " +
        "and is registered in the service provider DI system.";

    private readonly IReadOnlyMetaControllerCollection metaControllers;
    private readonly IServiceProvider provider;
    private readonly ILogger logger;

    public EmissionService(
        IReadOnlyMetaControllerCollection metaControllers,
        IServiceProvider provider,
        ILogger<EmissionService> logger)
    {
        this.metaControllers = metaControllers;
        this.provider = provider;
        this.logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var metaController in metaControllers)
        {
            if (metaController.Loop.AutoStart)
            {
                if (!provider.TryGetCastedService<ITelemetryController<ITelemetry>>(
                    serviceType: metaController.Type,
                    service: out var controller))
                {
                    logger.LogCritical(ControllerServiceNotFoundLog, metaController.Type);
                }
                else if (!cancellationToken.IsCancellationRequested)
                {
                    controller.Start();
                }
            }
        }
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var metaController in metaControllers)
        {
            if (!provider.TryGetCastedService<ITelemetryController<ITelemetry>>(
                serviceType: metaController.Type,
                service: out var controller))
            {
                logger.LogCritical(ControllerServiceNotFoundLog, metaController.Type);
            }
            else if (!cancellationToken.IsCancellationRequested)
            {
                controller.Stop(force: true);
            }
        }
        return Task.CompletedTask;
    }

}
