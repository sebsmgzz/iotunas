namespace IoTunas.Extensions.Connectivity.Collections;

using IoTunas.Core.Hosting;
using IoTunas.Extensions.Connectivity.Models;
using System.Reflection;
using System.Collections;

public class ConnectionObserverServiceCollection : IEnumerable<ConnectionObserverDescriptor>
{

    private readonly HashSet<ConnectionObserverDescriptor> descriptors;

    public int Count => descriptors.Count;

    public ConnectionObserverServiceCollection()
    {
        descriptors = new HashSet<ConnectionObserverDescriptor>();
    }

    public ConnectionObserverDescriptor Add(ConnectionObserverDescriptor descriptor)
    {
        return descriptors.Add(descriptor) ? descriptor : descriptors.First(d => d.Equals(descriptor));
    }

    public ConnectionObserverDescriptor Add(Type type)
    {
        var descriptor = new ConnectionObserverDescriptor(type);
        return Add(descriptor);
    }

    public ConnectionObserverDescriptor Add<TType>()
    {
        return Add(typeof(TType));
    }

    public void Map(Assembly assembly)
    {
        foreach (var type in assembly.GetDerivedTypes<IConnectionObserver>())
        {
            Add(type);
        }
    }

    public void Map()
    {
        var assembly = Assembly.GetEntryAssembly();
        Map(assembly!);
    }

    public IEnumerator<ConnectionObserverDescriptor> GetEnumerator()
    {
        var enumerable = (IEnumerable<ConnectionObserverDescriptor>)descriptors;
        return enumerable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        var enumerable = (IEnumerable)descriptors;
        return enumerable.GetEnumerator();
    }

}
