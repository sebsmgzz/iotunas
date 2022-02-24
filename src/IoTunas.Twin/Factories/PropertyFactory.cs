namespace IoTunas.Twin.Factories;

using IoTunas.Twin.Models;
using Microsoft.Extensions.DependencyInjection;

public class PropertyFactory<T> : IPropertyFactory<T> 
    where T : class, ITwinProperty
{

    private readonly IServiceProvider provider;
    private readonly IReadOnlyDictionary<string, Type> mapping;

    public IEnumerable<string> PropertyNames => mapping.Keys;

    public PropertyFactory(
        IServiceProvider provider, 
        IReadOnlyDictionary<string, Type> mapping)
    {
        this.provider = provider;
        this.mapping = mapping;
    }

    public bool TryGet(string propertyName, out T propertyModel)
    {
        if(!mapping.TryGetValue(propertyName, out var propertyType))
        {
            propertyModel = null;
            return false;
        }
        var scope = provider.CreateScope();
        var service = scope.ServiceProvider.GetService(propertyType);
        propertyModel = service as T;
        return propertyModel != null;
    }

}
