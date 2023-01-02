namespace IoTunas.Extensions.Methods.Models.Commands;

using IoTunas.Core.Seedwork;
using System.Collections.Generic;

public class DirectMethod : ValueObject
{

    public string Name { get; }

    public DirectMethod(string name)
    {
        Name = name;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }

}
