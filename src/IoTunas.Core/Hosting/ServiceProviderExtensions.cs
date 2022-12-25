namespace IoTunas.Core.Hosting;

using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

public static class ServiceProviderExtensions
{

    public static bool TryGetService(
        this IServiceProvider provider,
        Type serviceType,
        [MaybeNullWhen(false)] out object service)
    {
        using var scope = provider.CreateScope();
        service = scope.ServiceProvider.GetService(serviceType);
        return service != null;
    }

    public static bool TryGetService<TType>(
        this IServiceProvider provider,
        Type serviceType,
        [MaybeNullWhen(false)] out TType service)
        where TType : class
    {
        var isCreated = provider.TryGetService(serviceType, out var serviceObj);
        service = isCreated ? serviceObj as TType : null;
        return service != null;
    }

    public static bool TryGetService<TType>(
        this IServiceProvider provider,
        [MaybeNullWhen(false)] out TType service)
        where TType : class
    {
        return provider.TryGetService(typeof(TType), out service);
    }

}
