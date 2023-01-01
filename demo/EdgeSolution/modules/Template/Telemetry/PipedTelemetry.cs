namespace IoTunas.Demos.Template.Telemetry;

using IoTunas.Extensions.Telemetry.Models.Emission;
using Microsoft.Azure.Devices.Client;

public class PipedTelemetry : ITelemetry
{
    
    public Message Message { get; }

    public PipedTelemetry(Message message)
    {
        Message = message;
    }

    public Message AsMessage()
    {
        return Message;
    }

}
