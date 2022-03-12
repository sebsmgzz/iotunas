namespace IoTunas.Extensions.Connectivity.Mediators;

using Microsoft.Azure.Devices.Client;

public interface IConnectionMediator
{

    void HandleConnectionChange(
        ConnectionStatus status, ConnectionStatusChangeReason reason);

    Task HandleConnectionChangeAsync(
        ConnectionStatus status, ConnectionStatusChangeReason reason);

}
