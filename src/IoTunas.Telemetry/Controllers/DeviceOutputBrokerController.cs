namespace IoTunas.Extensions.Telemetry.Controllers;

using IoTunas.Extensions.Telemetry.Factories;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;

public class DeviceOutputBrokerController : OutputBrokerController
{

    private readonly DeviceClient client;

    public DeviceOutputBrokerController(
        DeviceClient client,
        IOutputBrokerFactory factory,
        ILogger<IOutputBrokerController> logger)
        : base(factory, logger)
    {
        this.client = client;
    }

    protected override async Task SendMessagesAsync(
        Message[] messages,
        CancellationToken cancellationToken)
    {
        if (messages.Length == 1)
        {
            await client.SendEventAsync(messages[0], cancellationToken);
        }
        else
        {
            await client.SendEventBatchAsync(messages, cancellationToken);
        }
    }

}
