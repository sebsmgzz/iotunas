namespace IoTunas.Extensions.Telemetry.Reflection;

using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class InputNameAttribute : Attribute
{

    public string Value { get; }

    public InputNameAttribute(string value)
    {
        Value = value;
    }

}

