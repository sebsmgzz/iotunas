namespace IoTunas.Telemetry.Mediators;
using IoTunas.Telemetry.Factories;
using IoTunas.Telemetry.Models;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;
using System;

public class InputTelemetryMediator : IInputTelemetryMediator
{

    private readonly ModuleClient moduleClient;
    private readonly ITelemetryEndpointFactory receiversFactory;
    private readonly ILogger logger;

    public InputTelemetryMediator(
        ModuleClient moduleClient,
        ITelemetryEndpointFactory receiversFactory,
        ILogger<IInputTelemetryMediator> logger)
    {
        this.moduleClient = moduleClient;
        this.receiversFactory = receiversFactory;
        this.logger = logger;
    }

    public async Task<MessageResponse> ReceiveAsync(Message message, object userContext)
    {
        logger.LogInformation($"Invoked | {message.InputName}");
        try
        {

            // First, check is receiver exists
            if (!receiversFactory.Contains(message.InputName))
            {
                logger.LogWarning($"Not found | {message.InputName}");
                await moduleClient.AbandonAsync(message.LockToken);
                return MessageResponse.Abandoned;
            }

            // Then, try to retrieve the receiver from the DI
            // If the receiver does not implements the interface
            // or it is not registered in the DI service provider
            // raise an exception because this should NEVER happen
            if (!receiversFactory.TryGet(message.InputName, out var receiver))
            {
                throw new InvalidCastException(
                    $"Receiver for '{message.InputName}' needs to " +
                    $"implement {nameof(IInputTelemetryBroker)} and be registered " +
                    $"in the service provider's DI to receive routed message.");
            }

            // Finally, process the message
            logger.LogInformation($"Received | {message.InputName}");
            return await receiver.ReceiveAsync(message, userContext);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error | {message.InputName}");
            await moduleClient.AbandonAsync(message.LockToken);
            return MessageResponse.Abandoned;
        }
    }

    public async Task CompleteAsync(string lockToken, CancellationToken cancellationToken = default)
    {
        await moduleClient.CompleteAsync(lockToken, cancellationToken);
    }

    public async Task CompleteAsync(Message message, CancellationToken cancellationToken = default)
    {
        await moduleClient.CompleteAsync(message, cancellationToken);
    }

    public async Task AbandonAsync(string lockToken, CancellationToken cancellationToken = default)
    {

        await moduleClient.AbandonAsync(lockToken, cancellationToken);
    }

    public async Task AbandonAsync(Message message, CancellationToken cancellationToken = default)
    {
        await moduleClient.AbandonAsync(message, cancellationToken);
    }

}
