namespace IoTunas.Demos.Template.Telemetry;

using IoTunas.Demos.Template.Services;
using IoTunas.Extensions.Telemetry.Emission;
using IoTunas.Extensions.Telemetry.Models.Reception;
using IoTunas.Extensions.Telemetry.Reflection;
using Microsoft.Azure.Devices.Client;
using System.Text;
using System.Threading.Tasks;

[TelemetryInput("input1")]
public class Input1Receiver : ITelemetryReceiver
{

    public const string Log = "Received message: {counterValue}, Body: [{messageString}]";

    private readonly ICounterService counter;
    private readonly ITelemetrySender sender;
    private readonly ILogger logger;

    public Input1Receiver(
        ICounterService counter,
        ITelemetrySender sender,
        ILogger<Input1Receiver> logger)
    {
        this.counter = counter;
        this.sender = sender;
        this.logger = logger;
    }

    public async Task<MessageResponse> HandleAsync(Message message, object userContext)
    {

        // Pipe telemetry
        var isSuccess = await sender.SendAsync<Output1Provider>(provider =>
        {
            provider.Message = message;
        });

        // React after telemetry has been sent
        if (isSuccess)
        {

            // Update counter
            var messageBytes = message.GetBytes();
            var messageString = Encoding.UTF8.GetString(messageBytes);
            var counterValue = counter.Increment();

            // Log and return result
            logger.LogInformation(Log, counterValue, messageString);
            return MessageResponse.Completed;

        }
        else
        {
            // TODO: Log it
            return MessageResponse.Abandoned;
        }

    }

}
