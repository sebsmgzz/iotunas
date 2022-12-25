namespace IoTunas.Extensions.Telemetry.Collections;

using IoTunas.Core.Hosting;
using IoTunas.Extensions.Telemetry.Models;
using IoTunas.Extensions.Telemetry.Reflection;
using System.Reflection;
using System.Collections;

public class ReceiverServiceCollection : IEnumerable<ReceiverDescriptor>
{

    private readonly HashSet<ReceiverDescriptor> descriptors;

    public int Count => descriptors.Count;

    public ReceiverServiceCollection()
    {
        descriptors = new HashSet<ReceiverDescriptor>();
    }

    public ReceiverDescriptor Add(ReceiverDescriptor descriptor)
    {
        return descriptors.Add(descriptor) ? descriptor : descriptors.First(d => d.Equals(descriptor));
    }

    public ReceiverDescriptor Add(Type type)
    {
        var descriptor = ReceiverDescriptorAttribute.GetDescriptor(type);
        return Add(descriptor);
    }

    public ReceiverDescriptor Add<TType>()
    {
        return Add(typeof(TType));
    }

    public ReceiverDescriptor Add(Type type, string methodName)
    {
        var descriptor = new ReceiverDescriptor(type, methodName);
        return Add(descriptor);
    }

    public ReceiverDescriptor Add<TType>(string methodName)
    {
        var descriptor = new ReceiverDescriptor(typeof(TType), methodName);
        return Add(descriptor);
    }

    public void Map(Assembly assembly)
    {
        foreach (var type in assembly.GetDerivedTypes<IEmissary>())
        {
            Add(type);
        }
    }

    public void Map()
    {
        var assembly = Assembly.GetEntryAssembly();
        Map(assembly!);
    }

    public IEnumerator<ReceiverDescriptor> GetEnumerator()
    {
        var enumerable = (IEnumerable<ReceiverDescriptor>)descriptors;
        return enumerable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        var enumerable = (IEnumerable)descriptors;
        return enumerable.GetEnumerator();
    }

}
