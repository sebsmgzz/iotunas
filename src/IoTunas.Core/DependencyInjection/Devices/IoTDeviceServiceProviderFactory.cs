namespace IoTunas.Core.DependencyInjection.Devices;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public class IoTDeviceServiceProviderFactory : IServiceProviderFactory<IIoTDeviceBuilder>
{

    private readonly HostBuilderContext context;

    public IoTDeviceServiceProviderFactory(HostBuilderContext context)
    {
        this.context = context;
    }

    public IIoTDeviceBuilder CreateBuilder(IServiceCollection services)
    {
        return new IoTDeviceBuilder(context, services);
    }

    public IServiceProvider CreateServiceProvider(IIoTDeviceBuilder containerBuilder)
    {
        return containerBuilder.BuildServiceProvider();
    }

}
