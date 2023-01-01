namespace IoTunas.Extensions.Telemetry.Models.Reception;

using IoTunas.Core.Seedwork;
using IoTunas.Extensions.Telemetry.Reflection;

public class MetaReceiver
{

    public InheritedType<ITelemetryReceiver> Type { get; }

    public TelemetryInput Input { get; }

    public MetaReceiver(Type type) : this(type, TelemetryInputAttribute.GetInputOrDefault(type))
    {
    }

    public MetaReceiver(Type type, TelemetryInput input)
    {
        Type = type;
        Input = input;
    }

}
