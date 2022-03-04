namespace IoTunas.Connectivity.Builders;

using IoTunas.Connectivity.Models;

public interface IConnectionObserversListBuilder
{

    public int Count { get; }

    void AddObserver<T>() where T : IConnectionObserver;
    
    void MapObservers();

    IReadOnlyList<Type> Build();

}
