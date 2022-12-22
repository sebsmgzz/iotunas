namespace IoTunas.Extensions.Connectivity.Factories;

using IoTunas.Extensions.Connectivity.Models;

public interface IConnectionObserverFactory
{
    
    IEnumerable<IConnectionObserver> GetAll();

    bool TryGet(int index, out IConnectionObserver observer);

}