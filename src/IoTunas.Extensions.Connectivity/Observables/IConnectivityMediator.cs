namespace IoTunas.Extensions.Connectivity.Observables;

using Microsoft.Azure.Devices.Client;

public interface IConnectivityMediator
{

    void HandleConnectionChange(
        ConnectionStatus status,
        ConnectionStatusChangeReason reason);

    Task HandleConnectionChangeAsync(
        ConnectionStatus status,
        ConnectionStatusChangeReason reason);

}
