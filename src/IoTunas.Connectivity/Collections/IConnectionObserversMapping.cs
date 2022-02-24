namespace IoTunas.Connectivity.Collections;

using IoTunas.Connectivity.Models;

public interface IConnectionObserversMapping
{

    void Add<T>() where T : IConnectionObserver;

    IReadOnlyList<Type> AsReadOnlyList();

}
