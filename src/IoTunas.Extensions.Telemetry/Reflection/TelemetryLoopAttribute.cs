namespace IoTunas.Extensions.Telemetry.Reflection;

using IoTunas.Extensions.Telemetry.Models.Emission;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class TelemetryLoopAttribute : Attribute
{

    public TimeSpan Period { get; }

    public bool AutoStart { get; }

    public TelemetryLoopAttribute() : this(TelemetryLoop.DefaultPeriod, TelemetryLoop.DefaultAutoStart)
    {
    }

    public TelemetryLoopAttribute(TimeSpan period) : this(period, TelemetryLoop.DefaultAutoStart)
    {
    }

    public TelemetryLoopAttribute(bool autoStart) : this(TelemetryLoop.DefaultPeriod, autoStart)
    {
    }

    public TelemetryLoopAttribute(TimeSpan period, bool autoStart)
    {
        Period = period;
        AutoStart = autoStart;
    }

    public TelemetryLoop AsLoop()
    {
        return new TelemetryLoop(Period, AutoStart);
    }

    public static TelemetryLoop GetLoopOrDefault(Type type)
    {
        return TryGetLoop(type, out var loop) ? loop : new TelemetryLoop();
    }

    public static TelemetryLoop GetLoopOrEmpty(Type type)
    {
        return TryGetLoop(type, out var loop) ? loop : TelemetryLoop.Empty;
    }

    public static bool TryGetLoop(
        Type type,
        [MaybeNullWhen(false)] out TelemetryLoop loop)
    {
        var attribute = type.GetCustomAttribute<TelemetryLoopAttribute>();
        if(attribute != null)
        {
            loop = attribute.AsLoop();
            return true;
        }
        else
        {
            loop = null;
            return false;
        }
    }

    public static bool IsDefined(Type type)
    {
        var attributeType = typeof(TelemetryLoopAttribute);
        return type.CustomAttributes.Any(a => a.AttributeType == attributeType);
    }

}
