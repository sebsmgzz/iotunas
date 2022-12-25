namespace IoTunas.Extensions.Commands.Hosting;

using IoTunas.Extensions.Commands.Hosting.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddCommandHandling(
        this IServiceCollection services,
        Action<ICommandsServiceBuilder>? configureAction = null)
    {
        var builder = new CommandsServiceBuilder();
        configureAction?.Invoke(builder);
        builder.Build(services);
        return services;
    }

}
