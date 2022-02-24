namespace IoTunas.Twin.Collections;

using IoTunas.Twin.Models;
using IoTunas.Twin.Reflection;
using System.Reflection;

public class TwinPropertyMapping<T> : ITwinPropertyMapping<T> where T : ITwinProperty
{

    private readonly Dictionary<string, Type> mapping = new();

    public void AddModel<U>(string propertyName) where U : T
    {
        mapping.Add(propertyName, typeof(U));
    }

    public void AddModel<U>() where U : T
    {
        var type = typeof(U);
        var attribute = type.GetCustomAttribute<TwinPropertyNameAttribute>();
        var propertyName = attribute?.Value ?? type.Name;
        AddModel<U>(propertyName);
    }

    public IReadOnlyDictionary<string, Type> AsReadOnlyDictionary()
    {
        return mapping;
    }

}
