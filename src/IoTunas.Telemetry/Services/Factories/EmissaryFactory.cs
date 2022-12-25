namespace IoTunas.Extensions.Telemetry.Factories;

using IoTunas.Extensions.Telemetry.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using IoTunas.Core.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

public class EmissaryFactory : IEmissaryFactory
{

    public const string InvalidEmissaryLog =
        "Emissary for {typeName} needs to " +
        "implement {interfaceName} and be registered " +
        "in the service provider's DI to send telemetry.";

    private readonly IReadOnlyDictionary<Type, EmissaryDescriptor> mapping;
    private readonly IServiceProvider provider;
    private readonly ILogger logger;

    public EmissaryFactory(
        IReadOnlyDictionary<Type, EmissaryDescriptor> mapping,
        IServiceProvider provider)
    {
        this.mapping = mapping;
        this.provider = provider;
        logger = provider.GetRequiredService<ILogger<IEmissaryFactory>>();
    }

    public bool TryGetValue(Type type, [MaybeNullWhen(false)] out IEmissary emissary)
    {
        if(!mapping.TryGetValue(type, out var descriptor))
        {
            emissary = null;
            return false;
        }
        if(!provider.TryGetService<IEmissary>(type, out emissary))
        {
            logger.LogCritical(InvalidEmissaryLog, type.Name, nameof(IEmissary));
            emissary = null;
            return false;
        }
        return emissary != null;
    }

    public bool TryGetValue<TType>([MaybeNullWhen(false)] out IEmissary emissary)
    {
        return TryGetValue(typeof(TType), out emissary);
    }

    public IEnumerable<IEmissary> GetAll()
    {
        foreach (var pair in mapping)
        {
            if (TryGetValue(pair.Value.Type, out var emissary))
            {
                yield return emissary;
            }
        }
    }

}
