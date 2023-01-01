namespace IoTunas.Core.DependencyInjection;

using System.Diagnostics.CodeAnalysis;

public static class ServiceProviderExtensions
{

    public static bool TryGetService(
        this IServiceProvider provider,
        Type serviceType,
        [MaybeNullWhen(false)] out object service)
    {
        service = provider.GetService(serviceType);
        return service != null;
    }

    public static TBase? GetCastedService<TBase>(
        this IServiceProvider provider,
        Type serviceType)
        where TBase : class
    {
        var service = provider.GetService(serviceType);
        return service as TBase;
    }

    public static bool TryGetCastedService<TBase>(
        this IServiceProvider provider,
        Type serviceType,
        [MaybeNullWhen(false)] out TBase service)
        where TBase : class
    {
        service = provider.GetCastedService<TBase>(serviceType);
        return service != null;
    }

    public static TBase? GetCastedService<TBase, TService>(
        this IServiceProvider provider)
        where TBase : class
    {
        return provider.GetCastedService<TBase>(typeof(TService));
    }

    public static bool TryGetCastedService<TBase, TService>(
        this IServiceProvider provider,
        [MaybeNullWhen(false)] out TBase service)
        where TBase : class
    {
        return provider.TryGetCastedService(typeof(TService), out service);
    }

}
