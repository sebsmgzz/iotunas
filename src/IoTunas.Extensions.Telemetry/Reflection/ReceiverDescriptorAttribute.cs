namespace IoTunas.Extensions.Telemetry.Reflection;

using IoTunas.Extensions.Telemetry.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class ReceiverDescriptorAttribute : Attribute
{
    public string InputName { get; }

    public ReceiverDescriptorAttribute(string inputName)
    {
        InputName = inputName;
    }

    public ReceiverDescriptor AsDescriptor(Type type)
    {
        return new ReceiverDescriptor(type, InputName);
    }

    public static ReceiverDescriptor GetDescriptor(Type type)
    {
        if (!TryGetDescriptor(type, out var descriptor))
        {
            descriptor = new ReceiverDescriptor(type, type.Name);
        }
        return descriptor;
    }

    public static bool TryGetDescriptor(
        Type type,
        [MaybeNullWhen(false)] out ReceiverDescriptor descriptor)
    {
        var attribute = type.GetCustomAttribute<ReceiverDescriptorAttribute>();
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
