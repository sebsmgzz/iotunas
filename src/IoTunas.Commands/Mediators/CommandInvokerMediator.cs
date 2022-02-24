namespace IoTunas.Commands.Mediators;

using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;

public class CommandInvokerMediator : ICommandInvokerMediator
{

    private readonly ModuleClient moduleClient;
    private readonly ILogger logger;

    public CommandInvokerMediator(
        ModuleClient moduleClient,
        ILogger<ICommandInvokerMediator> logger)
    {
        this.moduleClient = moduleClient;
        this.logger = logger;
    }

    public async Task<MethodResponse> InvokeAsync(
        string deviceId, MethodRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation($"Invoking | {request.Name}@{deviceId}");
        return await moduleClient.InvokeMethodAsync(
            deviceId, request, cancellationToken);
    }

    public async Task<MethodResponse> InvokeAsync(
        string deviceId, string moduleId, MethodRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation($"Invoking | {request.Name}@{deviceId}:{moduleId}");
        return await moduleClient.InvokeMethodAsync(
            deviceId, moduleId, request, cancellationToken);
    }

}

