namespace IoTunas.Extensions.Telemetry.Models.Reception;

using IoTunas.Core.Seedwork;
using IoTunas.Extensions.Telemetry.Reflection;
using System.Reflection;

public class MetaReceiver
{

    private readonly Lazy<TelemetryInput> input;

    public InheritedType<ITelemetryReceiver> Type { get; }

    public TelemetryInput Input => input.Value;

    public MetaReceiver(Type type) : this(new InheritedType<ITelemetryReceiver>(type))
    {
    }

    public MetaReceiver(InheritedType<ITelemetryReceiver> type)
    {
        Type = type;
        input = new Lazy<TelemetryInput>(CreateInput);
    }

    private TelemetryInput CreateInput()
    {
        return TelemetryInputAttribute.GetInputOrDefault(Type);
    }

    public static IEnumerable<MetaReceiver> GetAll(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (InheritedType<ITelemetryReceiver>.IsValid(type))
            {
                yield return new MetaReceiver(type);
            }
        }

    }

}
