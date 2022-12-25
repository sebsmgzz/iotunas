namespace IoTunas.Extensions.Telemetry.Factories;

using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;

public class MessageFactory : IMessageFactory
{

    public Message Create(byte[] bytesMessage)
    {
        return new Message(bytesMessage);
    }

    public Message Create(Stream streamMessage)
    {
        return new Message(streamMessage);
    }

    public Message Create(string stringMessage)
    {
        var bytesMessage = Encoding.ASCII.GetBytes(stringMessage);
        return Create(bytesMessage);
    }

    public Message Create(object? objectMessage)
    {
        string stringMessage = JsonConvert.SerializeObject(objectMessage);
        return Create(stringMessage);
    }

}