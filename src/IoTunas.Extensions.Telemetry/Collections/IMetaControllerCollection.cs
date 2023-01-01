namespace IoTunas.Extensions.Telemetry.Collections;

using IoTunas.Extensions.Telemetry.Emission;
using IoTunas.Extensions.Telemetry.Models.Emission;
using System;

public interface IMetaControllerCollection : IReadOnlyMetaControllerCollection
{

    bool Add(MetaController controller);

    bool Add(Type type, MetaProvider provider);

    bool Add<TType>(MetaProvider provider) where TType : ITelemetryController<ITelemetry>;

    bool Remove(MetaController controller);

    bool Remove(Type type);
    
    bool Remove<TType>() where TType : ITelemetryProvider<ITelemetry>;

}