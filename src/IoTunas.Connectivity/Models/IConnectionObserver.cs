namespace IoTunas.Connectivity.Models;

public interface IConnectionObserver
{

    Task HandleConnectionChangeAsync(ConnectionChangeArgs args);

}
