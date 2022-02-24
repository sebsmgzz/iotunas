namespace IoTunas.Connectivity.Models;

using Microsoft.Azure.Devices.Client;

public class ConnectionChangeArgs
{

    public ConnectionStatus Status { get; set; }

    public ConnectionStatusChangeReason Reason { get; set; }

    public ConnectionChangeArgs()
    {
    }

    public ConnectionChangeArgs(ConnectionStatus status, ConnectionStatusChangeReason reason)
    {
        Status = status;
        Reason = reason;
    }

}
