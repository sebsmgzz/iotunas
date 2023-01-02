namespace IoTunas.Core.Collections;

using System;
using System.Diagnostics.CodeAnalysis;

public interface IReadOnlyMetaTypeCollection<TMetaType> : IEnumerable<TMetaType>
{

    int Count { get; }

    TMetaType? Get(Type type);

    bool TryGet(Type type, [MaybeNullWhen(false)] out TMetaType meta);

}
