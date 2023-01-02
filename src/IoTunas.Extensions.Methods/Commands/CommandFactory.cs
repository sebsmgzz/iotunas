namespace IoTunas.Extensions.Methods.Commands;

using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using IoTunas.Core.DependencyInjection;
using IoTunas.Extensions.Methods.Models.Commands;
using IoTunas.Extensions.Methods.Collections;

public class CommandFactory : ICommandFactory
{

    public const string InvalidHandlerLog =
        "Handler for {methodName} needs to " +
        "implement {interfaceName} and be registered " +
        "in the service provider's DI to handle a direct method invocation.";

    private readonly IReadOnlyDictionary<string, Type> mapping;
    private readonly IServiceProvider provider;
    private readonly ILogger logger;

    public CommandFactory(
        IReadOnlyMetaCommandCollection commands,
        IServiceProvider provider,
        ILogger<ICommandFactory> logger)
    {
        this.provider = provider;
        this.logger = logger;
        mapping = commands.AsMapping();
    }

    public bool TryGet(string methodName, [MaybeNullWhen(false)] out ICommand command)
    {
        if (!mapping.TryGetValue(methodName, out var commandType))
        {
            command = null;
            return false;
        }
        if (!provider.TryGetCastedService<ICommand>(commandType, out var service))
        {
            logger.LogCritical(InvalidHandlerLog, methodName, nameof(ICommand));
            command = null;
            return false;
        }
        command = service;
        return true;
    }

}
