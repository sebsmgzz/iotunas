namespace IoTunas.Extensions.Telemetry.Collections;

using IoTunas.Extensions.Telemetry.Models.Reception;
using System;
using System.Reflection;

public interface IMetaReceiverCollection : IReadOnlyMetaReceiverCollection
{

    bool Add(MetaReceiver receiver);

    bool Add(Type type);

    bool Add<TType>() where TType : ITelemetryReceiver;

    void Map();

    void Map(Assembly assembly);

    bool Remove(MetaReceiver receiver);

    bool Remove(Type type);

    bool Remove<TType>() where TType : ITelemetryReceiver;

}