namespace IoTunas.Twin.Builders;

using IoTunas.Twin.Models;

public interface ITwinPropertyMappingBuilder<T> where T : ITwinProperty
{

    void AddModel<U>(string propertyName) where U : T;

    void AddModel<U>() where U : T;

    void MapModels();

}
