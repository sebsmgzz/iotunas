namespace IoTunas.Extensions.Telemetry.Models.Emission;

using IoTunas.Core.Seedwork;
using IoTunas.Extensions.Telemetry.Emission;
using IoTunas.Extensions.Telemetry.Reflection;
using System;
using System.Reflection;

public class MetaProvider
{

    private readonly Lazy<TelemetryOutput> output;

    public InheritedType<ITelemetryProvider<ITelemetry>> Type { get; }

    public TelemetryOutput Output => output.Value;

    public MetaProvider(Type type) : this(new InheritedType<ITelemetryProvider<ITelemetry>>(type))
    {
    }

    public MetaProvider(InheritedType<ITelemetryProvider<ITelemetry>> type)
    {
        Type = type;
        output = new Lazy<TelemetryOutput>(CreateOutput);
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

    private TelemetryOutput CreateOutput()
    {
        return TelemetryOutputAttribute.GetOutputOrDefault(Type);
    }

}
