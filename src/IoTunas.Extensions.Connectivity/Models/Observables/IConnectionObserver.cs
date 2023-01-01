namespace IoTunas.Extensions.Connectivity.Models.Observables;
public interface IConnectionObserver
{

    Task HandleConnectionChangeAsync(ConnectionChangeArgs args);

}
