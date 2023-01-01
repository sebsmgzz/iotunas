namespace IoTunas.Extensions.Telemetry.Models.Reception;

using IoTunas.Core.Seedwork;
using System.Collections.Generic;

public class TelemetryInput : ValueObject
{

    public string Name { get; }

    public TelemetryInput(string name)
    {
        Name = name;
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
