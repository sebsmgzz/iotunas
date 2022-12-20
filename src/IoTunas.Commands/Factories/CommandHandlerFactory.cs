namespace IoTunas.Extensions.Commands.Factories;

using IoTunas.Extensions.Commands.Collections;
using IoTunas.Extensions.Commands.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

public class CommandHandlerFactory : ICommandHandlerFactory
{

    private readonly IServiceProvider serviceProvider;
    private readonly CommandHandlerMapping handlersDefinition;

    public CommandHandlerFactory(
        IServiceProvider serviceProvider,
        CommandHandlerMapping handlersDefinitions)
    {
        this.serviceProvider = serviceProvider;
        this.handlersDefinition = handlersDefinitions;
    }

    public bool Contains(string methodName)
    {
        return handlersDefinition.Contains(methodName);
    }

    public bool TryGet(string methodName, [MaybeNullWhen(false)] out ICommandHandler handler)
    {
        if (!handlersDefinition.TryGetValue(methodName, out var handlerType))
        {
            handler = null;
            return false;
        }
        var scope = serviceProvider.CreateScope();
        var service = scope.ServiceProvider.GetService(handlerType);
        handler = service as ICommandHandler;
        return handler != null;
    }

}
