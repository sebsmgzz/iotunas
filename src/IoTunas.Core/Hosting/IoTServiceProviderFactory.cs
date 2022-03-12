namespace IoTunas.Core.Hosting;

using IoTunas.Core.Builders;
using IoTunas.Core.Builders.Containers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public class IoTServiceProviderFactory : IServiceProviderFactory<IIoTContainerBuilder>
{

    private readonly HostBuilderContext context;
    private readonly IoTContainer iotContainerType;

    private IoTServiceProviderFactory(
        HostBuilderContext context,
        IoTContainer iotContainerType)
    {
        this.context = context;
        this.iotContainerType = iotContainerType;
    }

    public static IoTServiceProviderFactory ForDevice(HostBuilderContext context)
    {
        return new IoTServiceProviderFactory(context, IoTContainer.Device);
    }

    public static IoTServiceProviderFactory ForModule(HostBuilderContext context)
    {
        return new IoTServiceProviderFactory(context, IoTContainer.Module);
    }

    public IIoTContainerBuilder CreateBuilder(IServiceCollection services)
    {
        return iotContainerType switch
        {
            IoTContainer.Device => new IoTDeviceBuilder(context, services),
            IoTContainer.Module => new IoTModuleBuilder(context, services),
            _ => throw new NotImplementedException(
                $"IoT container type not supported. " +
                $"Did forgot to update {nameof(IoTServiceProviderFactory)}?"),
        };
    }

    public IServiceProvider CreateServiceProvider(IIoTContainerBuilder containerBuilder)
    {
        return containerBuilder.BuildServiceProvider();
    }

}
