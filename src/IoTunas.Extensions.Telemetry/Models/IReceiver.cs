namespace IoTunas.Extensions.Telemetry.Models;

using Microsoft.Azure.Devices.Client;

public interface IReceiver
{

    Task<MessageResponse> HandleAsync(
        Message message,
        object userContext);

}
