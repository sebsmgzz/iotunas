namespace IoTunas.Extensions.Telemetry.Collections;

using System.Reflection;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using IoTunas.Extensions.Telemetry.Models.Reception;

public class MetaReceiverCollection : IMetaReceiverCollection
{

    private readonly HashSet<MetaReceiver> receivers;

    public int Count => receivers.Count;

    public MetaReceiverCollection()
    {
        receivers = new HashSet<MetaReceiver>();
    }

    public MetaReceiver? Get(Type type)
    {
        return receivers.FirstOrDefault(p => p?.Type.Equals(type) ?? false, null);
    }

    public bool TryGet(Type type, [MaybeNullWhen(false)] out MetaReceiver receiver)
    {
        receiver = Get(type);
        return receiver != null;
    }

    public bool Add(MetaReceiver receiver)
    {
        return receivers.Add(receiver);
    }

    public bool Add(Type type)
    {
        return Add(new MetaReceiver(type));
    }

    public bool Add<TType>() where TType : ITelemetryReceiver
    {
        return Add(typeof(TType));
    }

    public bool Remove(MetaReceiver receiver)
    {
        return receivers.Remove(receiver);
    }

    public bool Remove(Type type)
    {
        return TryGet(type, out var receiver) && receivers.Remove(receiver);
    }

    public bool Remove<TType>() where TType : ITelemetryReceiver
    {
        return Remove(typeof(TType));
    }

    public void Map(Assembly assembly)
    {
        foreach (var meta in MetaReceiver.GetAll(assembly))
        {
            Add(meta);
        }
    }

    public void Map()
    {
        var assembly = Assembly.GetEntryAssembly();
        Map(assembly!);
    }

    public Dictionary<string, Type> AsInputMapping()
    {
        var mapping = new Dictionary<string, Type>();
        foreach (var receiver in receivers)
        {
            mapping.Add(receiver.Input.Name, receiver.Type);
        }
        return mapping;
    }

    public IEnumerator<MetaReceiver> GetEnumerator()
    {
        var enumerable = (IEnumerable<MetaReceiver>)receivers;
        return enumerable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        var enumerable = (IEnumerable)receivers;
        return enumerable.GetEnumerator();
    }

}
