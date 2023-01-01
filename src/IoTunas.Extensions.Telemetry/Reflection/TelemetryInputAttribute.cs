namespace IoTunas.Extensions.Telemetry.Reflection;

using IoTunas.Extensions.Telemetry.Models.Reception;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class TelemetryInputAttribute : Attribute
{

    public string Name { get; }

    public TelemetryInputAttribute(string name)
    {
        Name = name;
    }

    public TelemetryInput AsInput()
    {
        return new TelemetryInput(Name);
    }

    public static TelemetryInput GetInputOrDefault(Type type)
    {
        if (!TryGetInput(type, out var input))
        {
            input = new TelemetryInput(type.Name);
        }
        return input;
    }

    public static bool TryGetInput(
        Type type,
        [MaybeNullWhen(false)] out TelemetryInput input)
    {
        var attribute = type.GetCustomAttribute<TelemetryInputAttribute>();
        if (attribute != null)
        {
            input = attribute.AsInput();
            return true;
        }
        else
        {
            input = null;
            return false;
        }
    }

}
