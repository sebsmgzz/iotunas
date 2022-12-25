namespace IoTunas.Extensions.Twin.Factories;

using IoTunas.Extensions.Twin.Models;

public interface IPropertyFactory<T> where T : ITwinProperty
{
    
    IEnumerable<string> PropertyNames { get; }

    bool TryGet(string propertyName, out T propertyModel);

}
