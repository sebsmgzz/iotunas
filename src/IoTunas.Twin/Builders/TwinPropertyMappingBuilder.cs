namespace IoTunas.Extensions.Twin.Builders;

using IoTunas.Extensions.Twin.Models;
using IoTunas.Extensions.Twin.Reflection;
using System.Reflection;

public class TwinPropertyMappingBuilder<T> : ITwinPropertyMappingBuilder<T> where T : ITwinProperty
{

    private readonly Dictionary<string, Type> mapping = new();

    private void AddModel(string propertyName, Type modelType)
    {
        if (!modelType.IsAssignableTo(typeof(T)))
        {
            throw new InvalidOperationException(
                $"A property model must implement the {typeof(T).Name}, " +
                $"which must inherit the {nameof(ITwinProperty)} interface, " +
                $"in order to be a property model.");
        }
        mapping.Add(propertyName, modelType);
    }

    public void AddModel<U>(string propertyName) where U : T
    {
        AddModel(propertyName, typeof(U));
    }

    private void AddModel(Type modelType)
    {
        var attribute = modelType.GetCustomAttribute<TwinPropertyNameAttribute>();
        var propertyName = attribute?.Value ?? modelType.Name;
        AddModel(propertyName, modelType);
    }

    public void AddModel<U>() where U : T
    {
        AddModel(typeof(U));
    }

    public void MapModels()
    {
        var interfaceType = typeof(T);
        var assembly = Assembly.GetEntryAssembly()!;
        var types = assembly.GetTypes();
        foreach (var modelType in types)
        {
            if (modelType.IsAssignableTo(interfaceType))
            {
                AddModel(modelType);
            }
        }
    }

    public IReadOnlyDictionary<string, Type> Build()
    {
        return new Dictionary<string, Type>(mapping);
    }

}
