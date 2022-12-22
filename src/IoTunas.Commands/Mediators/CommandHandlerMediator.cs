namespace IoTunas.Extensions.Commands.Mediators;

using IoTunas.Extensions.Commands.Factories;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;

public class CommandHandlerMediator : ICommandHandlerMediator
{

    public const string InvokedLog = "Invoked | {name}";
    public const string NotFoundLog = "Not found | {name}";
    public const string HandledLog = "Handled | {name}";
    public const string ErrorLog = "Error | {name}";

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

            // Try to get handler
            logger.LogInformation(InvokedLog, methodRequest.Name);
            if (!handlerFactory.TryGet(methodRequest.Name, out var handler))
            {
                logger.LogWarning(NotFoundLog, methodRequest.Name);
                return await HandleNotFoundAsync(methodRequest, userContext);
            }

            // Handle the invocation
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
