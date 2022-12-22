namespace IoTunas.Core.Collections;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

public abstract class Mapping<TKey, TDefinition, TInterface> :
    IMapping<TKey, TDefinition, TInterface>
    where TKey : notnull
{

    public readonly Dictionary<TKey, TDefinition> dictionary;

    public int Count => dictionary.Count;

    public TDefinition this[TKey key]
    {
        get => dictionary[key];
        set => dictionary[key] = value;
    }

    public Mapping()
    {
        dictionary = new Dictionary<TKey, TDefinition>();
    }

    protected abstract TDefinition CreateDefinition(TKey key, Type implementationType);

    protected abstract TKey CreateKey(Type implementationType);

    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TDefinition definition)
    {
        return dictionary.TryGetValue(key, out definition);
    }

    public TDefinition Add(TKey key, Type type)
    {
        var interfaceType = typeof(TInterface);
        if (!type.IsAssignableTo(interfaceType))
        {
            var thisType = GetType();
            throw new InvalidOperationException(
                $"Type {type.Name} must implement {interfaceType.Name} " +
                $"in order to be mapped in {thisType.Name}.");
        }
        var definition = CreateDefinition(key, type);
        dictionary.Add(key, definition);
        return definition;
    }

    public virtual TDefinition Add<TImplementation>(TKey key) where TImplementation : TInterface
    {
        return Add(key, typeof(TImplementation));
    }

    public virtual TDefinition Add(Type implementationType)
    {
        var key = CreateKey(implementationType);
        return Add(key, implementationType);
    }

    public virtual TDefinition Add<TImplementation>() where TImplementation : TInterface
    {
        return Add(typeof(TImplementation));
    }

    public void Map()
    {
        var assembly = Assembly.GetEntryAssembly();
        Map(assembly!);
    }

    public void Map(Assembly assembly)
    {
        var interfaceType = typeof(TInterface);
        var types = assembly.GetTypes();
        foreach (var implementation in types)
        {
            if (implementation.IsAssignableTo(interfaceType))
            {
                Add(implementation);
            }
        }
    }

    public IEnumerator<KeyValuePair<TKey, TDefinition>> GetEnumerator()
    {
        var enumerable = (IEnumerable<KeyValuePair<TKey, TDefinition>>)dictionary;
        return enumerable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        var enumerable = (IEnumerable)dictionary;
        return enumerable.GetEnumerator();
    }

}
