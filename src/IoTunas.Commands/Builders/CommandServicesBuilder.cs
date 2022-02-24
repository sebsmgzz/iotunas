namespace IoTunas.Commands.Builders;

using IoTunas.Commands.Collections;
using IoTunas.Commands.Factories;
using IoTunas.Commands.Mediators;
using Microsoft.Extensions.DependencyInjection;

public class CommandServicesBuilder : ICommandServicesBuilder
{

    private readonly IServiceCollection services;

    public CommandServicesBuilder(IServiceCollection services)
    {
        this.services = services;
    }

    public void AddHandlers(Action<ICommandHandlerMapping> configure)
    {
        var mapping = new CommandHandlerMapping();
        configure?.Invoke(mapping);
        var mappingDict = mapping.AsReadOnlyDictionary();
        foreach (var map in mappingDict)
        {
            services.AddScoped(map.Value);
        }
        services.AddSingleton<ICommandHandlerMediator, CommandHandlerMediator>();
        services.AddSingleton<IMethodResponseFactory, MethodResponseFactory>();
        services.AddSingleton<ICommandHandlerFactory, CommandHandlerFactory>(
            provider => new CommandHandlerFactory(provider, mappingDict));

    }

    public void AddInvokers()
    {
        services.AddSingleton<ICommandInvokerMediator, CommandInvokerMediator>();
    }

}
