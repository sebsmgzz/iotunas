namespace IoTunas.Extensions.Telemetry.Reflection;

using IoTunas.Extensions.Telemetry.Models;
using System;

[AttributeUsage(
    validOn: AttributeTargets.Class, 
    AllowMultiple = false, 
    Inherited = false)]
public class OutputBrokerDefinitionAttribute : Attribute
{

    public string TargetName { get; }

    public TimeSpan DefaultPeriod { get; set; }

    public bool AutoStart { get; set; }

    public OutputBrokerDefinitionAttribute() : this(OutputBrokerDefinition.IoTHubTargetName)
    {
    }

    public OutputBrokerDefinitionAttribute(string targetName)
    {
        TargetName = targetName;
        DefaultPeriod = TimeSpan.FromMinutes(OutputBrokerDefinition.DefaultPeriodMinutes);
        AutoStart = true;
    }

}
