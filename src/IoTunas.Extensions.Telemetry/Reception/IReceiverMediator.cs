namespace IoTunas.Extensions.Telemetry.Reception;

using Microsoft.Azure.Devices.Client;

public interface IReceiverMediator
{

    Task<MessageResponse> HandleAsync(
        Message message,
        object userContext);

}
