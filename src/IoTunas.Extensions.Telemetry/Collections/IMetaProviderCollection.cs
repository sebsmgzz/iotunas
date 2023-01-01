namespace IoTunas.Extensions.Telemetry.Collections;

using IoTunas.Extensions.Telemetry.Emission;
using IoTunas.Extensions.Telemetry.Models.Emission;
using System;
using System.Reflection;

public interface IMetaProviderCollection : IReadOnlyMetaProviderCollection
{

    bool Add(MetaProvider provider);

    bool Add(Type type);

    bool Remove(MetaProvider provider);

    bool Remove(Type type);

    bool Remove<TType>() where TType : ITelemetryProvider<ITelemetry>;

    void Map();

    void Map(Assembly assembly);

}
