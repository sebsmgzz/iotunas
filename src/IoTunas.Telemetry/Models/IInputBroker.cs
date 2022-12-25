namespace IoTunas.Extensions.Telemetry.Models;

using Microsoft.Azure.Devices.Client;

public interface IInputBroker
{

    Task<MessageResponse> HandleAsync(
        Message message, 
        object userContext);

}
