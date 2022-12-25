namespace IoTunas.Extensions.Commands.Hosting;

using IoTunas.Core.DependencyInjection.Builders;
using IoTunas.Extensions.Commands.Collections;
using IoTunas.Extensions.Commands.Factories;
using IoTunas.Extensions.Commands.Mediators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

public static class Extensions
{

    public static void UseCommandHandlers(
        this IoTDeviceBuilder device,
        Action<ICommandHandlerMapping>? configureAction)
    {
        device.Services.AddHostedService<DeviceCommandHandlingService>();
        device.Services.AddCommandHandlerAdHoc(configureAction);
    }

    public static void UseCommandHandlers(
        this IoTModuleBuilder module,
        Action<ICommandHandlerMapping>? configureAction)
    {
        module.Services.AddHostedService<ModuleCommandHandlingService>();
        module.Services.AddCommandHandlerAdHoc(configureAction);
    }

    private static void AddCommandHandlerAdHoc(
        this IServiceCollection services,
        Action<ICommandHandlerMapping>? configureAction)
    {
        var mapping = new CommandHandlerMapping();
        configureAction?.Invoke(mapping);
        services.AddSingleton<ICommandHandlerMapping>(mapping);
        foreach(var item in mapping)
        {
            services.AddScoped(item.Value);
        }
        services.TryAddSingleton<ICommandHandlerMediator, CommandHandlerMediator>();
        services.TryAddSingleton<IMethodResponseFactory, MethodResponseFactory>();
        services.TryAddSingleton<ICommandHandlerFactory, CommandHandlerFactory>();
    }

}
