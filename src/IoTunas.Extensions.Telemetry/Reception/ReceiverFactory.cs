namespace IoTunas.Extensions.Telemetry.Reception;

using IoTunas.Core.DependencyInjection;
using IoTunas.Extensions.Telemetry.Collections;
using IoTunas.Extensions.Telemetry.Models.Reception;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

public class ReceiverFactory : IReceiverFactory
{

    public const string InvalidBrokerLog =
        "Receiver for {inputName} needs to " +
        "implement {interfaceName} and be registered " +
        "in the service provider's DI to handle a input invocation.";

    private readonly IReadOnlyDictionary<string, Type> mapping;
    private readonly IServiceProvider provider;
    private readonly ILogger logger;

    public ReceiverFactory(
        IReadOnlyMetaReceiverCollection receivers,
        IServiceProvider provider,
        ILogger<IReceiverFactory> logger)
    {
        this.provider = provider;
        this.logger = logger;
        mapping = receivers.AsMapping();
    }

    public bool TryGet(string inputName, [MaybeNullWhen(false)] out ITelemetryReceiver broker)
    {
        if (!mapping.TryGetValue(inputName, out var type))
        {
            broker = null;
            return false;
        }
        if (!provider.TryGetCastedService(type, out broker))
        {
            logger.LogCritical(InvalidBrokerLog, inputName, nameof(ITelemetryReceiver));
            broker = null;
            return false;
        }
        return broker != null;
    }

}
