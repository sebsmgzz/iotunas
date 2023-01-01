namespace IoTunas.Extensions.Telemetry.Reception;

using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;

public class ReceiverMediator : IReceiverMediator
{

    public const string InvokedLog = "Invoked | {name}";
    public const string NotFoundLog = "Not found | {name}";
    public const string HandledLog = "Handled | {name}";
    public const string ErrorLog = "Error | {name}";

    private readonly IReceiverFactory factory;
    private readonly ILogger logger;

    public ReceiverMediator(
        IReceiverFactory factory,
        ILogger<IReceiverMediator> logger)
    {
        this.factory = factory;
        this.logger = logger;
    }

    public async Task<MessageResponse> HandleAsync(Message message, object userContext)
    {
        try
        {
            logger.LogInformation(InvokedLog, message.InputName);
            if (factory.TryGet(message.InputName, out var broker))
            {
                logger.LogInformation(HandledLog, message.InputName);
                return await broker.HandleAsync(message, userContext);
            }
            else
            {
                logger.LogWarning(NotFoundLog, message.InputName);
                return await BrokerNotFoundAsync(message, userContext);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ErrorLog, message.InputName);
            return await HandleErrorAsync(ex, message, userContext);
        }
    }

    protected virtual Task<MessageResponse> BrokerNotFoundAsync(
        Message message, object userContext)
    {
        return Task.FromResult(MessageResponse.Abandoned);
    }

    protected virtual Task<MessageResponse> HandleErrorAsync(
        Exception ex, Message message, object userContext)
    {
        return Task.FromResult(MessageResponse.Abandoned);
    }

}
