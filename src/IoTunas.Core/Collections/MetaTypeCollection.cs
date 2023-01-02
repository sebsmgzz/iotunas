namespace IoTunas.Core.Collections;

using IoTunas.Core.Reflection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Collections;

public abstract class MetaTypeCollection<TMetaType> : IMetaTypeCollection<TMetaType>
{


    protected readonly HashSet<TMetaType> items;

    public int Count => items.Count;

    public MetaTypeCollection()
    {
        items = new HashSet<TMetaType>();
    }

    public abstract TMetaType? Get(Type type);

    public bool TryGet(Type type, [MaybeNullWhen(false)] out TMetaType metaType)
    {
        metaType = Get(type);
        return metaType != null;
    }

    public bool Add(TMetaType metaType)
    {
        return items.Add(metaType);
    }

    public abstract bool Add(Type type);

    public bool Add<TType>() where TType : TMetaType
    {
        return Add(typeof(TType));
    }

    public bool Remove(TMetaType metaType)
    {
        return items.Remove(metaType);
    }

    public bool Remove(Type type)
    {
        return TryGet(type, out var metaType) && items.Remove(metaType);
    }

    public bool Remove<TType>() where TType : TMetaType
    {
        return Remove(typeof(TType));
    }

    public void Map(Assembly assembly)
    {
        var types = assembly.GetDerivedTypes<TMetaType>();
        foreach (var type in types)
        {
            Add(type);
        }
    }

    public void Map()
    {
        var assembly = Assembly.GetEntryAssembly();
        Map(assembly!);
    }

    public IEnumerator<TMetaType> GetEnumerator()
    {
        var enumerable = (IEnumerable<TMetaType>)items;
        return enumerable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        var enumerable = (IEnumerable)items;
        return enumerable.GetEnumerator();
    }

}
