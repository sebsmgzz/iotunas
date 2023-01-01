namespace IoTunas.Extensions.Telemetry.Reflection;

using IoTunas.Extensions.Telemetry.Models.Emission;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class TelemetryOutputAttribute : Attribute
{

    public string Name { get; }

    public TelemetryOutputAttribute(string name)
    {
        Name = name;
    }

    public TelemetryOutput AsOutput()
    {
        return new TelemetryOutput(Name);
    }

    public static implicit operator TelemetryOutput(TelemetryOutputAttribute attribute)
    {
        return attribute.AsOutput();
    }

    public static bool TryGetOutput(
        Type type,
        [MaybeNullWhen(false)] out TelemetryOutput output)
    {
        var attribute = type.GetCustomAttribute<TelemetryOutputAttribute>();
        if (attribute != null)
        {
            output = attribute.AsOutput();
            return true;
        }
        else
        {
            output = null;
            return false;
        }
    }

    public static TelemetryOutput GetOutputOrDefault(Type type)
    {
        if(TryGetOutput(type, out var output))
        {
            return output;
        }
        else
        {
            return TelemetryOutput.GetDefault(type);
        }
    }

}
