namespace IoTunas.Extensions.Telemetry.Reflection;

using IoTunas.Extensions.Telemetry.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class EmissaryDescriptorAttribute : Attribute
{

    public string OutputName { get; }

    public TimeSpan InitialPeriod { get; set; }

    public bool AutoStart { get; set; }

    public EmissaryDescriptorAttribute(string outputName)
    {
        OutputName = outputName;
        InitialPeriod = EmissaryDescriptor.DefaultPeriod;
        AutoStart = EmissaryDescriptor.DefaultAutoStart;
    }

    public EmissaryDescriptor AsDescriptor(Type type)
    {
        return new EmissaryDescriptor(type, OutputName, InitialPeriod, AutoStart);
    }

    public static EmissaryDescriptor GetDescriptor(Type type)
    {
        if (!TryGetDescriptor(type, out var descriptor))
        {
            descriptor = new EmissaryDescriptor(type, type.Name);
        }
        return descriptor;
    }

    public static bool TryGetDescriptor(
        Type type,
        [MaybeNullWhen(false)] out EmissaryDescriptor descriptor)
    {
        var attribute = type.GetCustomAttribute<EmissaryDescriptorAttribute>();
        if (attribute != null)
        {
            descriptor = attribute.AsDescriptor(type);
            return true;
        }
        else
        {
            descriptor = null;
            return false;
        }
    }

}
