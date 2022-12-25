namespace IoTunas.Extensions.Telemetry.Mediators;
using Microsoft.Azure.Devices.Client;

public interface IInputBrokerMediator
{

    Task<MessageResponse> HandleAsync(
        Message message,
        object userContext);

}
