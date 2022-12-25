namespace IoTunas.Core.DependencyInjection.Modules;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public class IoTModuleServiceProviderFactory : IServiceProviderFactory<IIoTModuleBuilder>
{

    private readonly HostBuilderContext context;

    public IoTModuleServiceProviderFactory(HostBuilderContext context)
    {
        this.context = context;
    }

    public IIoTModuleBuilder CreateBuilder(IServiceCollection services)
    {
        return new IoTModuleBuilder(context, services);
    }

    public IServiceProvider CreateServiceProvider(IIoTModuleBuilder containerBuilder)
    {
        return containerBuilder.BuildServiceProvider();
    }

}
