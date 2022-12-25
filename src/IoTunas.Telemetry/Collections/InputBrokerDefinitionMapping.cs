namespace IoTunas.Extensions.Telemetry.Collections;

using IoTunas.Core.Collections;
using IoTunas.Extensions.Telemetry.Models;
using IoTunas.Extensions.Telemetry.Reflection;
using System.Reflection;

public sealed class InputBrokerDefinitionMapping : Mapping<string, InputBrokerDefinition, IInputBroker>, IInputBrokerDefinitionMapping
{

    protected override InputBrokerDefinition CreateDefinition(string key, Type implementationType)
    {
        return new InputBrokerDefinition(key, implementationType);
    }

    protected override string CreateKey(Type implementationType)
    {
        var attribute = implementationType.GetCustomAttribute<InputBrokerDefinitionAttribute>();
        return attribute?.TargetName ?? implementationType.Name;
    }

}
