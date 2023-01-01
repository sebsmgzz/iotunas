namespace IoTunas.Extensions.Telemetry.Models.Emission;

using IoTunas.Core.Seedwork;
using System;
using System.Collections.Generic;

public class TelemetryLoop : ValueObject
{

    public readonly static TimeSpan DefaultPeriod = TimeSpan.FromMinutes(1);
    public readonly static bool DefaultAutoStart = true;
    public readonly static TelemetryLoop Empty = new(Timeout.InfiniteTimeSpan, false);

    public TimeSpan Period { get; }

    public bool AutoStart { get; }

    public TelemetryLoop() : this(DefaultPeriod, DefaultAutoStart)
    {
    }

    public TelemetryLoop(TimeSpan period, bool autoStart)
    {
        Period = period;
        AutoStart = autoStart;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Period;
        yield return AutoStart;
    }

    public override string ToString()
    {
        var periodStr = $"{nameof(Period)}: {Period}";
        var autoStartStr = $"{nameof(AutoStart)}: {AutoStart}";
        return $"{nameof(TelemetryLoop)} ({periodStr}, {autoStartStr})";
    }

}
