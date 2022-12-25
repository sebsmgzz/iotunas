namespace IoTunas.Extensions.Telemetry.Collections;

using IoTunas.Core.Collections;
using IoTunas.Extensions.Telemetry.Models;
using IoTunas.Extensions.Telemetry.Reflection;
using System.Reflection;

public sealed class OutputBrokerDefinitionMapping : Mapping<string, OutputBrokerDefinition, IOutputBroker>, IOutputBrokerDefinitionMapping
{

    protected override OutputBrokerDefinition CreateDefinition(string key, Type implementationType)
    {
        return new OutputBrokerDefinition(key, implementationType);
    }

    protected override string CreateKey(Type implementationType)
    {
        var attribute = implementationType.GetCustomAttribute<OutputBrokerDefinitionAttribute>();
        return attribute?.TargetName ?? implementationType.Name;
    }

}
