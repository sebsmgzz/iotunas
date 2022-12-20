namespace IoTunas.Core.Hosting;

using IoTunas.Core.Builders.Containers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public class IoTServiceProviderFactory : IServiceProviderFactory<IIoTContainerBuilder>
{

    private readonly HostBuilderContext context;
    private readonly IoTBuilderFactory builderFactory;

    private IoTServiceProviderFactory(
        HostBuilderContext context,
        IoTBuilderFactory builderFactory)
    {
        this.context = context;
        this.builderFactory = builderFactory;
    }

    public static IoTServiceProviderFactory ForDevice(HostBuilderContext context)
    {
        return new IoTServiceProviderFactory(context,
            (context, services) => new IoTDeviceBuilder(context, services));
    }

    public static IoTServiceProviderFactory ForModule(HostBuilderContext context)
    {
        return new IoTServiceProviderFactory(context,
            (context, services) => new IoTModuleBuilder(context, services));
    }

    public IIoTContainerBuilder CreateBuilder(IServiceCollection services)
    {
        return builderFactory.Invoke(context, services);
    }

    public IServiceProvider CreateServiceProvider(IIoTContainerBuilder containerBuilder)
    {
        return containerBuilder.BuildServiceProvider();
    }

}
