namespace IoTunas.Extensions.Telemetry.Models.Emission;

using IoTunas.Core.Seedwork;
using System;
using System.Collections.Generic;

public class TelemetryOutput : ValueObject
{

    public string Name { get; }

    public TelemetryOutput(string name)
    {
        Name = name;
    }

    public static TelemetryOutput GetDefault(Type type)
    {
        return new TelemetryOutput(type.Name);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }

    public override string ToString()
    {
        return Name;
    }

}
