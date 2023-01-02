namespace IoTunas.Core.Collections;

using System;
using System.Reflection;

public interface IMetaTypeCollection<TMetaType> : IReadOnlyMetaTypeCollection<TMetaType>
{

    bool Add(TMetaType meta);

    bool Add(Type type);

    bool Remove(TMetaType meta);

    bool Remove(Type type);

    bool Remove<TType>() where TType : TMetaType;

    void Map();

    void Map(Assembly assembly);

}
