namespace IoTunas.Connectivity.Collections;

using IoTunas.Connectivity.Models;
using System.Collections.Generic;

public class ConnectionObserversMapping : IConnectionObserversMapping
{

    private readonly List<Type> mapping = new();

    public void Add<T>() where T : IConnectionObserver
    {
        mapping.Add(typeof(T));
    }

    public IReadOnlyList<Type> AsReadOnlyList()
    {
        return mapping.AsReadOnly();
    }

}
