namespace IoTunas.Connectivity.Mediators;

using Microsoft.Azure.Devices.Client;

public interface IConnectionMediator
{

    Task HandleConnectionChangeAsync(
        ConnectionStatus status, ConnectionStatusChangeReason reason);

}
