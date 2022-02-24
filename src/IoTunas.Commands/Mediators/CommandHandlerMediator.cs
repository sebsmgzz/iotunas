namespace IoTunas.Commands.Mediators;

using IoTunas.Commands.Factories;
using IoTunas.Commands.Models;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;

public class CommandHandlerMediator : ICommandHandlerMediator
{

    private readonly IServiceProvider serviceProvider;
    private readonly ICommandHandlerFactory handlerfactory;
    private readonly IMethodResponseFactory responseFactory;
    private readonly ILogger logger;

    public CommandHandlerMediator(
        IServiceProvider serviceProvider,
        ICommandHandlerFactory handlerFactory,
        IMethodResponseFactory responseFactory,
        ILogger<ICommandHandlerMediator> logger)
    {
        this.serviceProvider = serviceProvider;
        handlerfactory = handlerFactory;
        this.responseFactory = responseFactory;
        this.logger = logger;
    }

    public async Task<MethodResponse> HandleAsync(
        MethodRequest methodRequest, object userContext)
    {
        logger.LogInformation($"Invoked | {methodRequest.Name}");
        try
        {

            // First, check if handler exists
            if (!handlerfactory.Contains(methodRequest.Name))
            {
                logger.LogWarning($"Not found | {methodRequest.Name}");
                return responseFactory.NotFound(new
                {
                    status = "not found"
                });
            }

            // Then try to get it
            // If the handler is not implementing the interface
            // or not being registered in the DI service provider
            // throw an exception, because this should NEVER happen
            if (!handlerfactory.TryGet(methodRequest.Name, out var handler))
            {
                throw new MissingMethodException(
                    $"Handler for {methodRequest.Name} needs to " +
                    $"implement {nameof(ICommandHandler)} and be registered " +
                    $"in the service provider's DI to handle a direct method invocation.");
            }

            // Finally, handle the invocation
            logger.LogInformation($"Handled | {methodRequest.Name}");
            return await handler.HandleAsync(methodRequest, userContext);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error | {methodRequest.Name}");
            return responseFactory.InternalError(new
            {
                status = "internal error",
                error = ex
            });
        }
    }

}
