namespace IoTunas.Extensions.Methods.Hosting;

using IoTunas.Extensions.Methods.Hosting.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddCommandHandling(this IServiceCollection services)
    {
        var builder = new CommandsServiceBuilder();
        builder.Commands.Map();
        builder.Build(services);
        return services;
    }

    public static IServiceCollection AddCommandHandling(
        this IServiceCollection services,
        Action<ICommandsServiceBuilder> configureAction)
    {
        var builder = new CommandsServiceBuilder();
        configureAction?.Invoke(builder);
        builder.Build(services);
        return services;
    }

}
