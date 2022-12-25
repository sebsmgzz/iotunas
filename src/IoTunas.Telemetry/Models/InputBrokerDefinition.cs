namespace IoTunas.Extensions.Telemetry.Models;

using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

public class InputBrokerDefinition
{

    public string TargetName { get; }

    public Type BrokerType { get; }

    public InputBrokerDefinition(string targetName, Type brokerType)
    {
        TargetName = targetName;
        BrokerType = brokerType;
    }

    public bool TryCreate(IServiceProvider provider, [MaybeNullWhen(false)] out IInputBroker broker)
    {
        var scope = provider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService(BrokerType);
        broker = service as IInputBroker;
        return broker != null;
    }

    public bool Equals(InputBrokerDefinition? definition)
    {
        return definition?.TargetName.Equals(TargetName) ?? false;
    }

    public override bool Equals(object? obj)
    {
        return obj is InputBrokerDefinition definition && Equals(definition);
    }

    public override int GetHashCode()
    {
        return TargetName.GetHashCode();
    }

    public override string ToString()
    {
        return $"[{nameof(InputBrokerDefinition)} {TargetName}]";
    }

    public static bool operator ==(
        InputBrokerDefinition? left, 
        InputBrokerDefinition? right)
    {
        return left?.Equals(right) ?? right is null;
    }

    public static bool operator !=(
        InputBrokerDefinition? left,
        InputBrokerDefinition? right)
    {
        return !(left == right);
    }

}
