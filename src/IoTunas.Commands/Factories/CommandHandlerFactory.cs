namespace IoTunas.Extensions.Commands.Factories;

using IoTunas.Extensions.Commands.Collections;
using IoTunas.Extensions.Commands.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

public class CommandHandlerFactory : ICommandHandlerFactory
{

    public const string InvalidHandlerLog =
        "Handler for {methodName} needs to " +
        "implement {interfaceName} and be registered " +
        "in the service provider's DI to handle a direct method invocation.";

    private readonly ICommandHandlerMapping mapping;
    private readonly IServiceProvider provider;
    private readonly ILogger logger;

    public CommandHandlerFactory(
        ICommandHandlerMapping mapping,
        IServiceProvider provider,
        ILogger<ICommandHandlerFactory> logger)
    {
        this.mapping = mapping;
        this.provider = provider;
        this.logger = logger;
    }

    public bool TryGet(string methodName, [MaybeNullWhen(false)] out ICommandHandler handler)
    {
        if (!mapping.TryGetValue(methodName, out var handlerType))
        {
            handler = null;
            return false;
        }
        var scope = provider.CreateScope();
        var service = scope.ServiceProvider.GetService(handlerType);
        handler = service as ICommandHandler;
        if(handler == null)
        {
            logger.LogCritical(InvalidHandlerLog, methodName, nameof(ICommandHandler));
        }
        return handler != null;
    }

}
