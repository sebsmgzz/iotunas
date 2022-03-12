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
        return Create(Encoding.ASCII.GetBytes(stringMessage));
    }

    public Message Create(object objectMessage)
    {
        return Create(JsonConvert.SerializeObject(objectMessage));
    }

}