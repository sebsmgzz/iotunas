namespace IoTunas.Extensions.Telemetry.Collections;

using IoTunas.Extensions.Telemetry.Models.Reception;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public interface IReadOnlyMetaReceiverCollection :IEnumerable<MetaReceiver>
{

    int Count { get; }

    MetaReceiver? Get(Type type);

    bool TryGet(Type type, [MaybeNullWhen(false)] out MetaReceiver receiver);

    Dictionary<string, Type> AsInputMapping();

}
