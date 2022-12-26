namespace IoTunas.Demos.Template.Telemetry;

using IoTunas.Extensions.Telemetry.Models;
using IoTunas.Extensions.Telemetry.Reflection;
using Microsoft.Azure.Devices.Client;
using System.Threading;
using System.Threading.Tasks;

[EmissaryDescriptor("output1", AutoStart = false)]
public class Output1Emissor : IEmissary
{

    public Message? Message { get; set; }

    public async Task<Message[]> HandleAsync(CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        if (Message != null)
        {
            var messageBytes = Message.GetBytes();
            var message = new Message(messageBytes);
            using var pipeMessage = new Message(messageBytes);
            foreach (var prop in message.Properties)
            {
                pipeMessage.Properties.Add(prop.Key, prop.Value);
            }
            return new Message[] { Message };
        }
        else
        {
            return Array.Empty<Message>();
        }
    }

}
