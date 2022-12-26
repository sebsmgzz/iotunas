namespace IoTunas.Demos.Template.Telemetry;

using IoTunas.Demos.Template.Services;
using IoTunas.Extensions.Telemetry.Factories;
using IoTunas.Extensions.Telemetry.Models;
using IoTunas.Extensions.Telemetry.Reflection;
using Microsoft.Azure.Devices.Client;
using System.Text;
using System.Threading.Tasks;

[ReceiverDescriptor("input1")]
public class Input1Receiver : IReceiver
{

    public const string Log = "Received message: {counterValue}, Body: [{messageString}]";

    private readonly ICounterService counter;
    private readonly IEmissaryControllerFactory factory;
    private readonly ILogger logger;


    public Input1Receiver(
        ICounterService counter, 
        IEmissaryControllerFactory factory,
        ILogger<Input1Receiver> logger)
    {
        this.counter = counter;
        this.factory = factory;
        this.logger = logger;
    }

    public Task<MessageResponse> HandleAsync(Message message, object userContext)
    {

        // Update counter
        var messageBytes = message.GetBytes();
        var messageString = Encoding.UTF8.GetString(messageBytes);
        var counterValue = counter.Increment();

        // Pipe message
        if(factory.TryGet(typeof(Output1Emissor), out var controller))
        {
            controller.SendMessage(new Output1Emissor()
            {
                Message = message
            });
        }

        // Log and return result
        logger.LogInformation(Log, counterValue, messageString);
        return Task.FromResult(MessageResponse.Completed);
    }

}
