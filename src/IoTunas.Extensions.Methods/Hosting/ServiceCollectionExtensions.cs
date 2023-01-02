namespace IoTunas.Extensions.Methods.Hosting;

using Microsoft.Extensions.DependencyInjection;
using System;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddMethodServices(
        this IServiceCollection services,
        Action<IMethodsServiceBuilder>? configureAction = null)
    {
        var builder = new MethodsServiceBuilder();
        configureAction?.Invoke(builder);
        builder.AddCommandServices(services);
        return services;
    }

    public static IServiceCollection AddCommandsOnly(
        this IServiceCollection services,
        Action<IMethodsServiceBuilder>? configureAction = null)
    {
        var builder = new MethodsServiceBuilder();
        configureAction?.Invoke(builder);
        builder.AddCommandServices(services);
        return services;
    }

}
