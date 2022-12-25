namespace IoTunas.Extensions.Telemetry.Controllers;

using IoTunas.Extensions.Telemetry.Factories;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;

public class ModuleOutputBrokerController : OutputBrokerController
{

    private readonly ModuleClient client;

    protected ModuleOutputBrokerController(
        ModuleClient client,
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
            await client.SendEventAsync(OutputName, messages[0], cancellationToken);
        }
        else
        {
            await client.SendEventBatchAsync(OutputName, messages, cancellationToken);
        }
    }

}
