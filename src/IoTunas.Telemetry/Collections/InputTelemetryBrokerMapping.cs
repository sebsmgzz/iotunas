namespace IoTunas.Telemetry.Receivers.Collections;

using IoTunas.Telemetry.Models;
using IoTunas.Telemetry.Reflection;
using System.Collections.Generic;
using System.Reflection;

public class InputTelemetryBrokerMapping : IInputTelemetryBrokerMapping
{

    private readonly Dictionary<string, Type> mapping = new();

    public void AddReceiver<T>(string inputName) where T : IInputTelemetryBroker
    {
        mapping.Add(inputName, typeof(T));
    }

    public void AddReceiver<T>() where T : IInputTelemetryBroker
    {
        AddReceiver(typeof(T));
    }

    // Keep it private to ensure type implements interface
    private void AddReceiver(Type receiverType)
    {
        var attribute = receiverType.GetCustomAttribute<InputNameAttribute>();
        var methodName = attribute?.Value ?? receiverType.Name;
        mapping.Add(methodName, receiverType);
    }

    public void MapReceivers()
    {
        var assembly = Assembly.GetEntryAssembly();
        var types = assembly.GetTypes();
        var receiverType = typeof(IInputTelemetryBroker);
        foreach (var type in types)
        {
            if (receiverType.IsAssignableFrom(type))
            {
                AddReceiver(receiverType);
            }
        }
    }

    public IReadOnlyDictionary<string, Type> AsReadOnlyDictionary()
    {
        return mapping;
    }

}