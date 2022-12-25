namespace IoTunas.Extensions.Telemetry.Factories;

using IoTunas.Core.Hosting;
using IoTunas.Extensions.Telemetry.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

public class ReceiverFactory : IReceiverFactory
{

    public const string InvalidBrokerLog =
        "Receiver for {inputName} needs to " +
        "implement {interfaceName} and be registered " +
        "in the service provider's DI to handle a input invocation.";

    private readonly Dictionary<string, Type> mapping;
    private readonly IServiceProvider provider;
    private readonly ILogger logger;

    public ReceiverFactory(
        Dictionary<string, Type> mapping,
        IServiceProvider provider)
    {
        this.mapping = mapping;
        this.provider = provider;
        logger = provider.GetRequiredService<ILogger<IReceiverFactory>>();
    }

    public bool TryGet(string inputName, [MaybeNullWhen(false)] out IReceiver broker)
    {
        if (!mapping.TryGetValue(inputName, out var type))
        {
            broker = null;
            return false;
        }
        if (!provider.TryGetService<IReceiver>(type, out broker))
        {
            logger.LogCritical(InvalidBrokerLog, inputName, nameof(IReceiver));
            broker = null;
            return false;
        }
        return broker != null;
    }

}
