namespace IoTunas.Extensions.Connectivity.Models;

public interface IConnectionObserver
{

    Task HandleConnectionChangeAsync(ConnectionChangeArgs args);

}
