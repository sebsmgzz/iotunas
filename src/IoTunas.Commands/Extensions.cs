namespace IoTunas.Extensions.Commands;

using IoTunas.Core.Builders.Containers;
using IoTunas.Extensions.Commands.Collections;
using IoTunas.Extensions.Commands.Factories;
using IoTunas.Extensions.Commands.Mediators;
using IoTunas.Extensions.Commands.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

public static class Extensions
{

    public static void UseCommandHandlers(
        this IoTDeviceBuilder device,
        Action<CommandHandlerMapping>? configureAction)
    {
        device.Services.AddHostedService<DeviceCommandHandlingService>();
        device.Services.AddCommandHandlerAdHoc(configureAction);
    }

    public static void UseCommandHandlers(
        this IoTModuleBuilder module,
        Action<CommandHandlerMapping>? configureAction)
    {
        module.Services.AddHostedService<ModuleCommandHandlingService>();
        module.Services.AddCommandHandlerAdHoc(configureAction);
    }

    private static void AddCommandHandlerAdHoc(
        this IServiceCollection services,
        Action<CommandHandlerMapping>? configureAction)
    {
        var mapping = new CommandHandlerMapping();
        configureAction?.Invoke(mapping);
        services.AddSingleton(mapping);
        services.TryAddSingleton<ICommandHandlerMediator, CommandHandlerMediator>();
        services.TryAddSingleton<IMethodResponseFactory, MethodResponseFactory>();
        services.TryAddSingleton<ICommandHandlerFactory, CommandHandlerFactory>();
    }

}
