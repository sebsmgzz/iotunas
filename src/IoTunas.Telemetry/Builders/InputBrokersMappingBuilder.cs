namespace IoTunas.Extensions.Telemetry.Builders;

using IoTunas.Extensions.Telemetry.Models;
using IoTunas.Extensions.Telemetry.Reflection;
using System.Collections.Generic;
using System.Reflection;

public class InputBrokersMappingBuilder : IInputBrokersMappingBuilder
{

    private readonly Dictionary<string, Type> mapping = new();

    private void AddReceiver(string inputName, Type receiverType)
    {
        if (!receiverType.IsAssignableTo(typeof(IInputTelemetryBroker)))
        {
            throw new InvalidOperationException(
                $"A receiver must implement the {nameof(IInputTelemetryBroker)} " +
                $"interface in order to receiver telemetry messages.");
        }
        mapping.Add(inputName, receiverType);
    }

    public void AddReceiver<T>(string inputName) where T : IInputTelemetryBroker
    {
        mapping.Add(inputName, typeof(T));
    }

    private void AddReceiver(Type receiverType)
    {
        var attribute = receiverType.GetCustomAttribute<InputNameAttribute>();
        var methodName = attribute?.Value ?? receiverType.Name;
        mapping.Add(methodName, receiverType);
    }

    public void AddReceiver<T>() where T : IInputTelemetryBroker
    {
        AddReceiver(typeof(T));
    }

    public void MapReceivers()
    {
        var interfaceType = typeof(IInputTelemetryBroker);
        var assembly = Assembly.GetEntryAssembly()!;
        var types = assembly.GetTypes();
        foreach (var receiverType in types)
        {
            if (interfaceType.IsAssignableFrom(receiverType))
            {
                AddReceiver(interfaceType);
            }
        }
    }

    public IReadOnlyDictionary<string, Type> Build()
    {
        return new Dictionary<string, Type>(mapping);
    }

}