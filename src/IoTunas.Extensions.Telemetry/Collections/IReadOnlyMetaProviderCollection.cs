namespace IoTunas.Extensions.Telemetry.Collections;

using IoTunas.Extensions.Telemetry.Models.Emission;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public interface IReadOnlyMetaProviderCollection : IEnumerable<MetaProvider>
{

    int Count { get; }

    MetaProvider? Get(Type type);

    bool TryGet(Type type, [MaybeNullWhen(false)] out MetaProvider provider);

}
