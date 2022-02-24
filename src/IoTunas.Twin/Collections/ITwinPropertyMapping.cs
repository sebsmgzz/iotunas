namespace IoTunas.Twin.Collections;

using IoTunas.Twin.Models;

public interface ITwinPropertyMapping<T> where T : ITwinProperty
{

    void AddModel<U>(string propertyName) where U : T;

    void AddModel<U>() where U : T;

}
