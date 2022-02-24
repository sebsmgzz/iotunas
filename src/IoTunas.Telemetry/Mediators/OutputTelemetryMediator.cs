namespace IoTunas.Telemetry.Mediators;

using IoTunas.Telemetry.Factories;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public class OutputTelemetryMediator : IOutputTelemetryMediator
{

    private readonly ModuleClient moduleClient;
    private readonly IMessageFactory messageFactory;
    private readonly ILogger logger;

    public OutputTelemetryMediator(
        ModuleClient moduleClient,
        IMessageFactory messageFactory,
        ILogger<IOutputTelemetryMediator> logger)
    {
        this.moduleClient = moduleClient;
        this.messageFactory = messageFactory;
        this.logger = logger;
    }

    public async Task SendAsync(
        Message message,
        string? outputName = null,
        CancellationToken cancellation = default)
    {
        var byteCount = message.GetBytes().Length;
        if (string.IsNullOrWhiteSpace(outputName))
        {
            logger.LogInformation($"Sending {byteCount} bytes.");
            await moduleClient.SendEventAsync(message, cancellation);
        }
        else
        {
            logger.LogInformation($"Sending {byteCount} bytes at {outputName}.");
            await moduleClient.SendEventAsync(outputName, message, cancellation);
        }
    }

    public async Task SendAsync(
        byte[] messageBytes,
        string? outputName = null,
        Action<Message>? configureMessage = null,
        CancellationToken cancellation = default)
    {
        var message = messageFactory.Create(messageBytes);
        configureMessage?.Invoke(message);
        await SendAsync(message, outputName, cancellation);
    }

    public async Task SendAsync(
        Stream messageStream,
        string? outputName = null,
        Action<Message>? configureMessage = null,
        CancellationToken cancellation = default)
    {
        var message = messageFactory.Create(messageStream);
        configureMessage?.Invoke(message);
        await SendAsync(message, outputName, cancellation);
    }

    public async Task SendAsync(
        string messageString,
        string? outputName = null,
        Action<Message>? configureMessage = null,
        CancellationToken cancellation = default)
    {
        var message = messageFactory.Create(messageString);
        configureMessage?.Invoke(message);
        await SendAsync(message, outputName, cancellation);
    }

    public async Task SendAsync(
        object messageObject,
        string? outputName = null,
        Action<Message>? configureMessage = null,
        CancellationToken cancellation = default)
    {
        var message = messageFactory.Create(messageObject);
        configureMessage?.Invoke(message);
        await SendAsync(message, outputName, cancellation);
    }

}
