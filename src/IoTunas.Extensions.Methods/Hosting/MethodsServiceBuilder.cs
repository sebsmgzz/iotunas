namespace IoTunas.Extensions.Methods.Hosting;

using IoTunas.Extensions.Methods.Collections;
using IoTunas.Extensions.Methods.Commands;
using Microsoft.Extensions.DependencyInjection;

public class MethodsServiceBuilder : IMethodsServiceBuilder
{

    public IMetaCommandCollection Commands { get; }

    public MethodsServiceBuilder()
    {
        Commands = new MetaCommandCollection();
    }

    public void AddCommandServices(IServiceCollection services)
    {
        foreach (var command in Commands)
        {
            services.AddScoped(command.Type);
        }
        services.AddHostedService<CommandsService>();
        services.AddSingleton<ICommandMediator, CommandMediator>();
        services.AddSingleton<ICommandFactory, CommandFactory>();
        services.AddSingleton<IMethodResponseFactory, MethodResponseFactory>();
    }

}
