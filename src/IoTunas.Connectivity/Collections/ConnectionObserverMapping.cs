namespace IoTunas.Extensions.Connectivity.Builders;

using IoTunas.Extensions.Connectivity.Models;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class ConnectionObserverMapping : IEnumerable<Type>
{

    private readonly List<Type> types;

    public int Count => types.Count;

    public Type this[int i] => types[i];

    public ConnectionObserverMapping()
    {
        types = new List<Type>();
    }

    public void AddObserver(Type observerType)
    {
        if (!observerType.IsAssignableTo(typeof(IConnectionObserver)))
        {
            throw new InvalidOperationException(
                $"An observer must implement the {nameof(IConnectionObserver)} " +
                $"interface in order to observe the connection.");
        }
        types.Add(observerType);
    }

    public void AddObserver<T>() where T : IConnectionObserver
    {
        AddObserver(typeof(T));
    }

    public void MapObservers(Assembly assembly)
    {
        var interfaceType = typeof(IConnectionObserver);
        var types = assembly.GetTypes();
        foreach (var observerType in types)
        {
            if (observerType.IsAssignableTo(interfaceType))
            {
                AddObserver(observerType);
            }
        }
    }

    public void MapObservers()
    {
        var assembly = Assembly.GetEntryAssembly();
        MapObservers(assembly!);
    }

    public IEnumerator<Type> GetEnumerator()
    {
        var enumerable = (IEnumerable<Type>)types;
        return enumerable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        var enumerable = (IEnumerable)types;
        return enumerable.GetEnumerator();
    }

}
