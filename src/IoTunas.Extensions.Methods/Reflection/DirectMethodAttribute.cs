namespace IoTunas.Extensions.Methods.Reflection;

using IoTunas.Extensions.Methods.Models.Commands;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class DirectMethodAttribute : Attribute
{

    public string Name { get; }

    public DirectMethodAttribute(string name)
    {
        Name = name;
    }

    public DirectMethod AsDirectMethod()
    {
        return new DirectMethod(Name);
    }

    public static DirectMethod GetDirectMethodOrDefault(Type type)
    {
        if(!TryGetDescriptor(type, out var directMethod))
        {
            directMethod = new DirectMethod(type.Name);
        }
        return directMethod;
    }

    public static bool TryGetDescriptor(
        Type type, 
        [MaybeNullWhen(false)] out DirectMethod directMethod)
    {
        var attribute = type.GetCustomAttribute<DirectMethodAttribute>();
        if(attribute != null)
        {
            directMethod = attribute.AsDirectMethod();
            return true;
        }
        else
        {
            directMethod = null;
            return false;
        }
    }

}
