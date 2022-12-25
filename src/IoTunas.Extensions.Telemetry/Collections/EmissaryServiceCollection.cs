namespace IoTunas.Extensions.Telemetry.Collections;

using IoTunas.Core.Hosting;
using IoTunas.Extensions.Telemetry.Models;
using IoTunas.Extensions.Telemetry.Reflection;
using System.Reflection;
using System.Collections;

public class EmissaryServiceCollection : IEnumerable<EmissaryDescriptor>
{

    private readonly HashSet<EmissaryDescriptor> descriptors;

    public int Count => descriptors.Count;

    public EmissaryServiceCollection()
    {
        descriptors = new HashSet<EmissaryDescriptor>();
    }

    public EmissaryDescriptor Add(EmissaryDescriptor descriptor)
    {
        return descriptors.Add(descriptor) ? descriptor : descriptors.First(d => d.Equals(descriptor));
    }

    public EmissaryDescriptor Add(Type type)
    {
        var descriptor = EmissaryDescriptorAttribute.GetDescriptor(type);
        return Add(descriptor);
    }

    public EmissaryDescriptor Add<TType>()
    {
        return Add(typeof(TType));
    }

    public EmissaryDescriptor Add(Type type, string methodName)
    {
        var descriptor = new EmissaryDescriptor(type, methodName);
        return Add(descriptor);
    }

    public EmissaryDescriptor Add<TType>(string methodName)
    {
        var descriptor = new EmissaryDescriptor(typeof(TType), methodName);
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

    public IEnumerator<EmissaryDescriptor> GetEnumerator()
    {
        var enumerable = (IEnumerable<EmissaryDescriptor>)descriptors;
        return enumerable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        var enumerable = (IEnumerable)descriptors;
        return enumerable.GetEnumerator();
    }

}
