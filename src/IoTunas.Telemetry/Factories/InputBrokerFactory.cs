namespace IoTunas.Extensions.Telemetry.Factories;

using IoTunas.Extensions.Telemetry.Collections;
using IoTunas.Extensions.Telemetry.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

public class InputBrokerFactory : IInputBrokerFactory
{

    public const string InvalidBrokerLog =
        "Broker for {inputName} needs to " +
        "implement {interfaceName} and be registered " +
        "in the service provider's DI to handle a input invocation.";

    private readonly IInputBrokerDefinitionMapping mapping;
    private readonly IServiceProvider provider;
    private readonly ILogger logger;

    public InputBrokerFactory(
        IInputBrokerDefinitionMapping mapping,
        IServiceProvider provider,
        ILogger<IInputBrokerFactory> logger)
    {
        this.mapping = mapping;
        this.provider = provider;
        this.logger = logger;
    }

    public bool TryGet(string inputName, [MaybeNullWhen(false)] out IInputBroker broker)
    {
        if (!mapping.TryGetValue(inputName, out var brokerDefinition))
        {
            broker = null;
            return false;
        }
        var scope = provider.CreateScope();
        var service = scope.ServiceProvider.GetService(brokerDefinition.BrokerType);
        broker = service as IInputBroker;
        if (broker == null)
        {
            logger.LogCritical(InvalidBrokerLog, inputName, nameof(IInputBroker));
        }
        return broker != null;
    }

}
