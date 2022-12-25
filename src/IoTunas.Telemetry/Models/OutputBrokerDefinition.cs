namespace IoTunas.Extensions.Telemetry.Models;

using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

public class OutputBrokerDefinition
{

    public const string IoTHubTargetName = "$upstream";
    public const int DefaultPeriodMinutes = 3;

    public string TargetName { get; }

    public Type BrokerType { get; }

    public TimeSpan DefaultPeriod { get; set; }

    public bool AutoStart { get; set; }

    public OutputBrokerDefinition(Type brokerType) : 
        this(IoTHubTargetName, brokerType)
    {
    }

    public OutputBrokerDefinition(string targetName, Type brokerType)
    {
        TargetName = targetName;
        BrokerType = brokerType;
    }

    public bool TryCreate(IServiceProvider provider, [MaybeNullWhen(false)] out IOutputBroker broker)
    {
        var scope = provider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService(BrokerType);
        broker = service as IOutputBroker;
        return broker != null;
    }

    public bool Equals(OutputBrokerDefinition? definition)
    {
        return definition?.TargetName.Equals(TargetName) ?? false;
    }

    public override bool Equals(object? obj)
    {
        return obj is OutputBrokerDefinition definition && Equals(definition);
    }

    public override int GetHashCode()
    {
        return TargetName.GetHashCode();
    }

    public override string ToString()
    {
        return $"[{nameof(OutputBrokerDefinition)} {TargetName}]";
    }

    public static bool operator ==(
        OutputBrokerDefinition? left,
        OutputBrokerDefinition? right)
    {
        return left?.Equals(right) ?? right is null;
    }

    public static bool operator !=(
        OutputBrokerDefinition? left,
        OutputBrokerDefinition? right)
    {
        return !(left == right);
    }

}
