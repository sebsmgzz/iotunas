namespace IoTunas.Extensions.Commands.Reflection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class CommandNameAttribute : Attribute
{

    public string Value { get; }

    public CommandNameAttribute(string value)
    {
        Value = value;
    }

}

