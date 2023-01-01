namespace IoTunas.Extensions.Telemetry.Models.Emission;

using IoTunas.Core.Seedwork;
using IoTunas.Extensions.Telemetry.Emission;
using IoTunas.Extensions.Telemetry.Reflection;
using System;

public class MetaController
{

    private readonly Lazy<TelemetryLoop> loop;

    public InheritedType<ITelemetryController<ITelemetry>> Type { get; }

    public MetaProvider Provider { get; }

    public TelemetryLoop Loop => loop.Value;

    public MetaController(Type type, MetaProvider provider)
        : this(new InheritedType<ITelemetryController<ITelemetry>>(type), provider)
    {
    }

    public MetaController(InheritedType<ITelemetryController<ITelemetry>> type, MetaProvider provider)
    {
        Type = type;
        Provider = provider;
        loop = new Lazy<TelemetryLoop>(CreateLoop);
    }

    private TelemetryLoop CreateLoop()
    {
        return TelemetryLoopAttribute.GetLoopOrEmpty(Type.Value);
    }

    public static bool HasLoop(MetaProvider provider)
    {
        return TelemetryLoopAttribute.IsDefined(provider.Type);
    }

}
