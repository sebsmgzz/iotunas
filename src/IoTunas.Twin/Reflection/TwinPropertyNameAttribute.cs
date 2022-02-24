namespace IoTunas.Twin.Reflection;

using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class TwinPropertyNameAttribute : Attribute
{

    public string Value { get; }

    public TwinPropertyNameAttribute(string value)
    {
        Value = value;
    }

}
