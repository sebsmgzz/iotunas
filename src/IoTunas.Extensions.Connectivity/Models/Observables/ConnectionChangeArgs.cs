namespace IoTunas.Extensions.Connectivity.Models.Observables;

using Microsoft.Azure.Devices.Client;

public class ConnectionChangeArgs
{

    public ConnectionStatus Status { get; }

    public ConnectionStatusChangeReason Reason { get; }

    public ConnectionChangeArgs(
        ConnectionStatus status,
        ConnectionStatusChangeReason reason)
    {
        Status = status;
        Reason = reason;
    }

    public override string ToString()
    {
        return $"[{Status} {Reason}]";
    }

}
