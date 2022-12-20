namespace IoTunas.Extensions.Commands.Mediators;

using IoTunas.Extensions.Commands.Factories;
using IoTunas.Extensions.Commands.Models;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;

public class CommandHandlerMediator : ICommandHandlerMediator
{

    public const string InvokedLog = "Invoked | {name}";
    public const string NotFoundLog = "Not found | {name}";
    public const string HandledLog = "Handled | {name}";
    public const string ErrorLog = "Error | {name}";
    public const string InvalidHandlerLog =
        "Handler for {methodRequestName} needs to " +
        "implement {interfaceName} and be registered " +
        "in the service provider's DI to handle a direct method invocation.";

    private readonly ICommandHandlerFactory handlerFactory;
    private readonly IMethodResponseFactory responseFactory;
    private readonly ILogger logger;

    public CommandHandlerMediator(
        ICommandHandlerFactory handlerFactory,
        IMethodResponseFactory responseFactory,
        ILogger<ICommandHandlerMediator> logger)
    {
        this.handlerFactory = handlerFactory;
        this.responseFactory = responseFactory;
        this.logger = logger;
    }

    public async Task<MethodResponse> HandleAsync(
        MethodRequest methodRequest, object userContext)
    {
        try
        {

            // First, check if handler exists
            logger.LogInformation(InvokedLog, methodRequest.Name);
            if (!handlerFactory.Contains(methodRequest.Name))
            {
                logger.LogWarning(NotFoundLog, methodRequest.Name);
                return await HandleNotFoundAsync(methodRequest, userContext);
            }

            // Then try to get it
            // If the handler is not implementing the interface
            // or not being registered in the DI service provider
            // throw an exception, because this should NEVER happen
            if (!handlerFactory.TryGet(methodRequest.Name, out var handler))
            {
                logger.LogCritical(InvalidHandlerLog, methodRequest.Name, nameof(ICommandHandler));
                return await HandleInvalidHandlerAsync(methodRequest, userContext);
            }

            // Finally, handle the invocation
            logger.LogInformation(HandledLog, methodRequest.Name);
            return await handler.HandleAsync(methodRequest, userContext);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, ErrorLog, methodRequest.Name);
            return await HandleErrorAsync(ex, methodRequest, userContext);
        }
    }

    protected virtual async Task<MethodResponse> HandleNotFoundAsync(
        MethodRequest methodRequest, object userContext)
    {
        return await Task.FromResult(responseFactory.NotFound(new
        {
            status = "not found",
            name = methodRequest.Name
        }));
    }

    protected virtual async Task<MethodResponse> HandleInvalidHandlerAsync(
        MethodRequest methodRequest, object userContext)
    {
        return await Task.FromResult(responseFactory.InternalError(new
        {
            status = "invalid handler",
            name = methodRequest.Name
        }));
    }

    protected virtual async Task<MethodResponse> HandleErrorAsync(
        Exception ex, MethodRequest methodRequest, object userContext)
    {
        return await Task.FromResult(responseFactory.InternalError(new
        {
            status = "internal error",
            name = methodRequest.Name,
            message = ex.Message
        }));
    }

}
