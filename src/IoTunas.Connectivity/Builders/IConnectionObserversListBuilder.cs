namespace IoTunas.Extensions.Connectivity.Builders;

using IoTunas.Extensions.Connectivity.Models;

public interface IConnectionObserversListBuilder
{

    public int Count { get; }

    void AddObserver<T>() where T : IConnectionObserver;
    
    void MapObservers();

    IReadOnlyList<Type> Build();

}
