namespace IoTunas.Core.Collections;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

public interface IMapping<TKey, TDefinition, TInterface> : IEnumerable<KeyValuePair<TKey, TDefinition>> 
{

    int Count { get; }

    TDefinition this[TKey key] { get; set; }

    bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TDefinition definition);

    TDefinition Add(TKey key, Type type);

    TDefinition Add<TImplementation>(TKey key) where TImplementation : TInterface;

    TDefinition Add(Type implementationType);

    TDefinition Add<TImplementation>() where TImplementation : TInterface;

    void Map();

    void Map(Assembly assembly);

}
