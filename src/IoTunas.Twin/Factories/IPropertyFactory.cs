namespace IoTunas.Twin.Factories;

using IoTunas.Twin.Models;

public interface IPropertyFactory<T> where T : ITwinProperty
{
    
    IEnumerable<string> PropertyNames { get; }

    bool TryGet(string propertyName, out T propertyModel);

}
