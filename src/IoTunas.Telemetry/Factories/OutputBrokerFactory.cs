namespace IoTunas.Extensions.Telemetry.Factories;

using IoTunas.Extensions.Telemetry.Models;
using System;
using IoTunas.Extensions.Telemetry.Collections;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

public class OutputBrokerFactory : IOutputBrokerFactory
{

    private readonly IServiceProvider provider;
    private readonly IOutputBrokerDefinitionMapping mapping;

    public OutputBrokerFactory(
        IServiceProvider provider,
        IOutputBrokerDefinitionMapping mapping)
    {
        this.provider = provider;
        this.mapping = mapping;
    }

    public bool TryGetValue(Type type, [MaybeNullWhen(false)] out IOutputBroker broker)
    {
        var scope = provider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService(type);
        broker = service as IOutputBroker;
        return broker != null;
    }

    public bool TryGetValue<TType>([MaybeNullWhen(false)] out IOutputBroker broker)
    {
        return TryGetValue(typeof(TType), out broker);
    }

    public IEnumerable<IOutputBroker> GetAll()
    {
        foreach (var pair in mapping)
        {
            if (TryGetValue(pair.Value.BrokerType, out var broker))
            {
                yield return broker;
            }
        }
    }

}
