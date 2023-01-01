namespace IoTunas.Extensions.Telemetry.Collections;

using IoTunas.Extensions.Telemetry.Models.Emission;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public interface IReadOnlyMetaControllerCollection : IEnumerable<MetaController>
{

    int Count { get; }

    MetaController? Get(Type type);

    bool TryGet(Type type, [MaybeNullWhen(false)] out MetaController controller);

}
