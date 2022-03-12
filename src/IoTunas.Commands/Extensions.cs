namespace IoTunas.Extensions.Commands;

using IoTunas.Core.Builders.Containers;
using IoTunas.Extensions.Commands.Builders;
using IoTunas.Extensions.Commands.Factories;
using IoTunas.Extensions.Commands.Mediators;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class Extensions
{

    public static void UseCommandInvokers(this IoTModuleBuilder module)
    {
        module.Services.AddSingleton<ICommandInvokerMediator, CommandInvokerMediator>();
    }

    public static void UseCommandHandlers(
        this IoTDeviceBuilder device,
        Action<ICommandHandlerMappingBuilder> configureAction)
    {

        // Add handlers
        device.Services.AddCommandHandlerAdHoc(configureAction);

        // Add post config
        device.AfterBuild(async provider =>
        {
            var client = provider.GetRequiredService<DeviceClient>();
            var mediator = provider.GetRequiredService<ICommandHandlerMediator>();
            await client.SetMethodDefaultHandlerAsync(mediator.HandleAsync, null, default);
        });

    }

    public static void UseCommandHandlers(
        this IoTModuleBuilder module,
        Action<ICommandHandlerMappingBuilder> configureAction)
    {

        // Add handlers
        module.Services.AddCommandHandlerAdHoc(configureAction);

        // Add post config
        module.AfterBuild(async provider =>
        {
            var client = provider.GetRequiredService<ModuleClient>();
            var mediator = provider.GetRequiredService<ICommandHandlerMediator>();
            await client.SetMethodDefaultHandlerAsync(mediator.HandleAsync, null, default);
        });

    }

    private static void AddCommandHandlerAdHoc(
        this IServiceCollection services,
        Action<ICommandHandlerMappingBuilder> configureAction)
    {

        // Add handlers
        var builder = new CommandHandlerMappingBuilder();
        configureAction.Invoke(builder);
        var mapping = builder.Build();
        foreach (var pair in mapping)
        {
            services.AddScoped(pair.Value);
        }

        // Add command services
        services.AddSingleton<ICommandHandlerMediator, CommandHandlerMediator>();
        services.AddSingleton<IMethodResponseFactory, MethodResponseFactory>();
        services.AddSingleton<ICommandHandlerFactory, CommandHandlerFactory>(
            provider => new CommandHandlerFactory(provider, mapping));

    }

}
