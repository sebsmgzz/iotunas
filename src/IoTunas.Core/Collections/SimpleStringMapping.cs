namespace IoTunas.Core.Collections;

public class SimpleStringMapping<TInterface> : Mapping<string, TInterface, Type>
{

    protected override Type CreateDefinition(string key, Type implementationType)
    {
        return implementationType;
    }

    protected override string CreateKey(Type implementationType)
    {
        return implementationType.Name;
    }

}
