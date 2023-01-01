namespace IoTunas.Extensions.Telemetry.Collections;

using System.Reflection;
using System.Collections;
using IoTunas.Extensions.Telemetry.Models.Emission;
using System.Diagnostics.CodeAnalysis;
using IoTunas.Extensions.Telemetry.Emission;

public class MetaProviderCollection : IMetaProviderCollection
{

    private readonly HashSet<MetaProvider> providers;

    public int Count => providers.Count;

    public MetaProviderCollection()
    {
        providers = new HashSet<MetaProvider>();
    }

    public MetaProvider? Get(Type type)
    {
        return providers.FirstOrDefault(p => p?.Type.Equals(type) ?? false, null);
    }

    public bool TryGet(Type type, [MaybeNullWhen(false)] out MetaProvider provider)
    {
        provider = Get(type);
        return provider != null;
    }

    public bool Add(MetaProvider provider)
    {
        return providers.Add(provider);
    }

    public bool Add(Type type)
    {
        return Add(new MetaProvider(type));
    }

    public bool Add<TType>() where TType : ITelemetryProvider<ITelemetry>
    {
        return Add(typeof(TType));
    }

    public bool Remove(MetaProvider provider)
    {
        return providers.Remove(provider);
    }

    public bool Remove(Type type)
    {
        return TryGet(type, out var provider) && providers.Remove(provider);
    }

    public bool Remove<TType>() where TType : ITelemetryProvider<ITelemetry>
    {
        return Remove(typeof(TType));
    }

    public void Map(Assembly assembly)
    {
        foreach (var meta in MetaProvider.GetAll(assembly))
        {
            Add(meta);
        }
    }

    public void Map()
    {
        var assembly = Assembly.GetEntryAssembly();
        Map(assembly!);
    }

    public IEnumerator<MetaProvider> GetEnumerator()
    {
        var enumerable = (IEnumerable<MetaProvider>)providers;
        return enumerable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        var enumerable = (IEnumerable)providers;
        return enumerable.GetEnumerator();
    }

}
