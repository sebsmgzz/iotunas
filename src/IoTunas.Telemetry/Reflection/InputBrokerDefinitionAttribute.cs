namespace IoTunas.Extensions.Telemetry.Reflection;

using System;

[AttributeUsage(
    validOn: AttributeTargets.Class, 
    AllowMultiple = false, 
    Inherited = false)]
public class InputBrokerDefinitionAttribute : Attribute
{

    public string TargetName { get; }

    public InputBrokerDefinitionAttribute(string targetName)
    {
        TargetName = targetName;
    }

}
