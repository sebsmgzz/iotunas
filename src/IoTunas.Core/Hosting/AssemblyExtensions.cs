namespace IoTunas.Core.Hosting;
using System.Reflection;

public static class AssemblyExtensions
{

    public static IEnumerable<Type> GetDerivedTypes(
        this Assembly assembly,
        Type baseType)
    {
        var types = assembly.GetTypes();
        foreach (var type in types)
        {
            if (type.IsAssignableTo(baseType))
            {
                yield return type;
            }
        }
    }

    public static IEnumerable<Type> GetDerivedTypes<TBaseType>(this Assembly assembly)
    {
        return assembly.GetDerivedTypes(typeof(TBaseType));
    }

}
