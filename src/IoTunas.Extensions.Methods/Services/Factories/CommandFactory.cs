namespace IoTunas.Extensions.Methods.Services.Factories;

using IoTunas.Extensions.Methods.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using IoTunas.Core.Hosting;

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
        IReadOnlyDictionary<string, Type> mapping,
        IServiceProvider provider)
    {
        this.mapping = mapping;
        this.provider = provider;
        logger = provider.GetRequiredService<ILogger<ICommandFactory>>();
    }

    public bool TryGet(string methodName, [MaybeNullWhen(false)] out ICommand command)
    {
        if (!mapping.TryGetValue(methodName, out var commandType))
        {
            command = null;
            return false;
        }
        if(!provider.TryGetService<ICommand>(commandType, out var service))
        {
            logger.LogCritical(InvalidHandlerLog, methodName, nameof(ICommand));
            command = null;
            return false;
        }
        command = service;
        return true;
    }

}
