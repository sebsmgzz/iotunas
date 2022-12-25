namespace IoTunas.Core.Hosting;

using IoTunas.Core.DependencyInjection;
using IoTunas.Core.DependencyInjection.Devices;
using IoTunas.Core.DependencyInjection.Modules;
using IoTunas.Core.Services.ClientHosts.Devices;
using IoTunas.Core.Services.ClientHosts.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

public static class HostBuilderExtensions
{

    public static IHostBuilder ConfigureIoTModuleDefaults(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .UseServiceProviderFactory(context => new IoTModuleServiceProviderFactory(context))
            .ConfigureAppConfiguration(config =>
            {
                config.AddEnvironmentVariables(IoTModuleHost.IoTVariablePrefix);
            });
    }

    public static IHostBuilder ConfigureIoTModuleDefaults(
        this IHostBuilder hostBuilder,
        Action<IIoTModuleBuilder> configureAction)
    {
        return hostBuilder
            .ConfigureIoTModuleDefaults()
            .ConfigureContainer<IIoTModuleBuilder>(configureAction);
    }

    public static IHostBuilder ConfigureIoTDeviceDefaults(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .UseServiceProviderFactory(context => new IoTDeviceServiceProviderFactory(context))
            .ConfigureAppConfiguration(config =>
            {
                config.AddEnvironmentVariables(IoTDeviceHost.IoTVariablePrefix);
            });
    }

    public static IHostBuilder ConfigureIoTDeviceDefaults(
        this IHostBuilder hostBuilder,
        Action<IIoTDeviceBuilder> configureAction)
    {
        return hostBuilder
            .ConfigureIoTDeviceDefaults()
            .ConfigureContainer<IIoTDeviceBuilder>(configureAction);
    }

}
