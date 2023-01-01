namespace IoTunas.Extensions.Connectivity.Collections;

using System.Reflection;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using IoTunas.Core.Reflection;
using IoTunas.Extensions.Connectivity.Models.Observables;

public class MetaObserverCollection : IMetaObserverCollection
{

    private readonly HashSet<MetaObserver> observers;

    public int Count => observers.Count;

    public MetaObserverCollection()
    {
        observers = new HashSet<MetaObserver>();
    }

    public MetaObserver? Get(Type type)
    {
        return observers.FirstOrDefault(o => o?.Type.Equals(type) ?? false, null);
    }

    public bool TryGet(Type type, [MaybeNullWhen(false)] out MetaObserver observer)
    {
        observer = Get(type);
        return observer != null;
    }

    public bool Add(MetaObserver observer)
    {
        return observers.Add(observer);
    }

    public bool Add(Type type)
    {
        return Add(new MetaObserver(type));
    }

    public bool Add<TType>() where TType : IConnectionObserver
    {
        return Add(typeof(TType));
    }

    public bool Remove(MetaObserver observer)
    {
        return observers.Remove(observer);
    }

    public bool Remove(Type type)
    {
        return TryGet(type, out var observer) && observers.Remove(observer);
    }

    public bool Remove<TType>() where TType : IConnectionObserver
    {
        return Add(typeof(TType));
    }

    public void Map(Assembly assembly)
    {
        var types = assembly.GetDerivedTypes<IConnectionObserver>();
        foreach (var type in types)
        {
            Add(type);
        }
    }

    public void Map()
    {
        var assembly = Assembly.GetEntryAssembly();
        Map(assembly!);
    }

    public IEnumerator<MetaObserver> GetEnumerator()
    {
        var enumerable = (IEnumerable<MetaObserver>)observers;
        return enumerable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        var enumerable = (IEnumerable)observers;
        return enumerable.GetEnumerator();
    }

}
