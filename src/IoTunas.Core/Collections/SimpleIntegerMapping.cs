namespace IoTunas.Core.Collections;

public class SimpleIntegerMapping<TInterface> : Mapping<int, Type, TInterface>
{

    protected override Type CreateDefinition(int key, Type implementationType)
    {
        return implementationType;
    }

    protected override int CreateKey(Type implementationType)
    {
        return Count;
    }

}
