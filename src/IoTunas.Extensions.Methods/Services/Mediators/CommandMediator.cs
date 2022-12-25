namespace IoTunas.Extensions.Commands.Services.Mediators;

using IoTunas.Extensions.Commands.Services.Factories;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;

public class CommandMediator : ICommandMediator
{

    public const string InvokedLog = "Invoked | {name}";
    public const string NotFoundLog = "Not found | {name}";
    public const string HandledLog = "Handled | {name}";
    public const string ErrorLog = "Error | {name}";

    private readonly ICommandFactory commands;
    private readonly IMethodResponseFactory responses;
    private readonly ILogger logger;

    public CommandMediator(
        ICommandFactory commands,
        IMethodResponseFactory responses,
        ILogger<ICommandMediator> logger)
    {
        this.commands = commands;
        this.responses = responses;
        this.logger = logger;
    }

    public async Task<MethodResponse> HandleAsync(
        MethodRequest methodRequest, object userContext)
    {
        try
        {

            // Try to get the command
            logger.LogInformation(InvokedLog, methodRequest.Name);
            if (!commands.TryGet(methodRequest.Name, out var handler))
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
        return await Task.FromResult(responses.NotFound(new
        {
            status = "not found",
            name = methodRequest.Name
        }));
    }

    protected virtual async Task<MethodResponse> HandleErrorAsync(
        Exception ex, MethodRequest methodRequest, object userContext)
    {
        return await Task.FromResult(responses.InternalError(new
        {
            status = "internal error",
            name = methodRequest.Name,
            message = ex.Message
        }));
    }

}
