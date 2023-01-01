namespace IoTunas.Core.Reflection;

using System.Reflection;

public static class AssemblyExtensions
{

    public static IEnumerable<Type> GetDerivedTypes(
        this Assembly assembly,
        Type baseType)
    {
        return assembly.GetTypes().Where(type => type.IsAssignableTo(baseType));
    }

    public static IEnumerable<Type> GetDerivedTypes<TBaseType>(this Assembly assembly)
    {
        return assembly.GetDerivedTypes(typeof(TBaseType));
    }

}
