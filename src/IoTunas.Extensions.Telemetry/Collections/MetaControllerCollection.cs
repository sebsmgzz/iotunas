namespace IoTunas.Extensions.Telemetry.Collections;

using System.Collections;
using IoTunas.Extensions.Telemetry.Models.Emission;
using System.Diagnostics.CodeAnalysis;
using IoTunas.Extensions.Telemetry.Emission;

public class MetaControllerCollection : IMetaControllerCollection
{

    private readonly HashSet<MetaController> controllers;

    public int Count => controllers.Count;

    public MetaControllerCollection()
    {
        controllers = new HashSet<MetaController>();
    }

    public MetaController? Get(Type type)
    {
        return controllers.FirstOrDefault(controller => controller?.Type.Equals(type) ?? false, null);
    }

    public bool TryGet(Type type, [MaybeNullWhen(false)] out MetaController controller)
    {
        controller = Get(type);
        return controller != null;
    }

    public bool Add(MetaController controller)
    {
        return controllers.Add(controller);
    }

    public bool Add(Type type, MetaProvider provider)
    {
        return Add(new MetaController(type, provider));
    }

    public bool Add<TType>(MetaProvider provider) where TType : ITelemetryController<ITelemetry>
    {
        return Add(typeof(TType), provider);
    }

    public bool Remove(MetaController controller)
    {
        return controllers.Remove(controller);
    }

    public bool Remove(Type type)
    {
        return TryGet(type, out var provider) && controllers.Remove(provider);
    }

    public bool Remove<TType>() where TType : ITelemetryProvider<ITelemetry>
    {
        return Remove(typeof(TType));
    }

    public IEnumerator<MetaController> GetEnumerator()
    {
        var enumerable = (IEnumerable<MetaController>)controllers;
        return enumerable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        var enumerable = (IEnumerable)controllers;
        return enumerable.GetEnumerator();
    }

}