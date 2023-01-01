namespace IoTunas.Extensions.Telemetry.Models.Emission;

using IoTunas.Core.Seedwork;
using IoTunas.Extensions.Telemetry.Emission;
using IoTunas.Extensions.Telemetry.Reflection;
using System;

public class MetaProvider
{

    public InheritedType<ITelemetryProvider<ITelemetry>> Type { get; }

    public TelemetryOutput Output { get; }

    public MetaProvider(Type type) : this(type, TelemetryOutputAttribute.GetOutputOrDefault(type))
    {
    }

    public MetaProvider(Type type, TelemetryOutput output)
    {
        Type = type;
        Output = output;
    }

    internal Type GetControllerAbstraction()
    {
        var propertyName = nameof(ITelemetryProvider<ITelemetry>.ControllerAbstraction);
        var propertyInfo = Type.Value.GetProperty(propertyName);
        var type = propertyInfo?.GetValue(null) as Type;
        return type!;
    }

    internal Type GetControllerImplementation()
    {
        var propertyName = nameof(ITelemetryProvider<ITelemetry>.ControllerImplementation);
        var propertyInfo = Type.Value.GetProperty(propertyName);
        var type = propertyInfo?.GetValue(null) as Type;
        return type!;
    }

}
