namespace IoTunas.Extensions.Commands.Hosting.Commands;

using IoTunas.Extensions.Commands.Collections;
using IoTunas.Extensions.Commands.Factories;
using IoTunas.Extensions.Commands.Mediators;
using Microsoft.Extensions.DependencyInjection;

public class CommandServiceBuilder : ICommandServiceBuilder
{

    public CommandServiceCollection Commands { get; }

    public CommandServiceBuilder()
    {
        Commands = new CommandServiceCollection();
    }

    public void Build(IServiceCollection services)
    {
        var mapping = new Dictionary<string, Type>();
        foreach (var descriptor in Commands)
        {
            services.AddScoped(descriptor.Type);
            mapping.Add(descriptor.MethodName, descriptor.Type);
        }
        services.AddHostedService<CommandsHost>();
        services.AddSingleton<ICommandMediator, CommandMediator>();
        services.AddSingleton<IMethodResponseFactory, MethodResponseFactory>();
        services.AddSingleton<ICommandFactory, CommandFactory>(p => new CommandFactory(mapping, p));
    }

}
