namespace IoTunas.Extensions.Connectivity.Observables;

using IoTunas.Extensions.Connectivity.Models.Observables;

public interface IConnectionObserverFactory
{

    IEnumerable<IConnectionObserver> GetAll();

    bool TryGet(int index, out IConnectionObserver observer);

}