namespace IoTunas.Connectivity.Builders;

using IoTunas.Connectivity.Models;
using System.Collections.Generic;
using System.Reflection;

public class ConnectionObserversListBuilder : IConnectionObserversListBuilder
{

    private readonly List<Type> types = new();

    public int Count => types.Count;    

    private void AddObserver(Type observerType)
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

    public void MapObservers()
    {
        var interfaceType = typeof(IConnectionObserver);
        var assembly = Assembly.GetEntryAssembly()!;
        var types = assembly.GetTypes();
        foreach (var observerType in types)
        {
            if (observerType.IsAssignableTo(interfaceType))
            {
                AddObserver(observerType);
            }
        }
    }

    public IReadOnlyList<Type> Build()
    {
        return types.AsReadOnly();
    }

}
