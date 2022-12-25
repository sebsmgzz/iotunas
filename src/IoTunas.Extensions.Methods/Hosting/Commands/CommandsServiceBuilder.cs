namespace IoTunas.Extensions.Methods.Hosting.Commands;

using IoTunas.Extensions.Methods.Collections;
using IoTunas.Extensions.Methods.Services.Factories;
using IoTunas.Extensions.Methods.Services.Mediators;
using Microsoft.Extensions.DependencyInjection;

public class CommandsServiceBuilder : ICommandsServiceBuilder
{

    public CommandServiceCollection Commands { get; }

    public CommandsServiceBuilder()
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
