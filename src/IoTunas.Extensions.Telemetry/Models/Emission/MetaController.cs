namespace IoTunas.Extensions.Telemetry.Models.Emission;

using IoTunas.Core.Seedwork;
using IoTunas.Extensions.Telemetry.Emission;
using IoTunas.Extensions.Telemetry.Reflection;
using System;

public class MetaController
{

    public InheritedType<ITelemetryController<ITelemetry>> Type { get; }

    public MetaProvider Provider { get; }

    public TelemetryLoop Loop { get; }

    public MetaController(Type type, MetaProvider provider) 
        : this(type, provider, TelemetryLoopAttribute.GetLoopOrEmpty(type))
    {
    }

    public MetaController(Type type, MetaProvider provider, TelemetryLoop loop)
    {
        Type = type;
        Provider = provider;
        Loop = loop;
    }

    public static bool HasLoop(MetaProvider provider)
    {
        return TelemetryLoopAttribute.IsDefined(provider.Type);
    }

}
