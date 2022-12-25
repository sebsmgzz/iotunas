namespace IoTunas.Extensions.Methods.Reflection;

using IoTunas.Extensions.Methods.Models;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class CommandDescriptorAttribute : Attribute
{

    public string MethodName { get; }

    public CommandDescriptorAttribute(string value)
    {
        MethodName = value;
    }

    public CommandDescriptor AsDescriptor(Type type)
    {
        return new CommandDescriptor(type, MethodName);
    }

    public static CommandDescriptor GetDescriptor(Type type)
    {
        if(!TryGetDescriptor(type, out var descriptor))
        {
            descriptor = new CommandDescriptor(type, type.Name);
        }
        return descriptor;
    }

    public static bool TryGetDescriptor(
        Type type, 
        [MaybeNullWhen(false)] out CommandDescriptor descriptor)
    {
        var attribute = type.GetCustomAttribute<CommandDescriptorAttribute>();
        if(attribute != null)
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
