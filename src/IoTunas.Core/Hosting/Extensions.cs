namespace IoTunas.Core.Hosting;

using IoTunas.Core.Builders.Containers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

public static class Extensions
{

    public const string IoTDeviceVariablePrefix = "IOT_";
    public const string IoTModuleVariablePrefix = "IOTEDGE_";

    public static IHostBuilder ConfigureIoTModuleDefaults(
        this IHostBuilder hostBuilder,
        Action<IoTModuleBuilder> configureAction)
    {
        return hostBuilder
            .ConfigureAppConfiguration(config =>
                config.AddEnvironmentVariables(IoTModuleVariablePrefix))
            .UseServiceProviderFactory(
                IoTServiceProviderFactory.ForModule)
            .ConfigureContainer<IoTModuleBuilder>(container =>
                configureAction?.Invoke(container));
    }

    public static IHostBuilder ConfigureIoTDeviceDefaults(
        this IHostBuilder hostBuilder,
        Action<IoTDeviceBuilder> configureAction)
    {
        return hostBuilder
            .ConfigureAppConfiguration(config =>
                config.AddEnvironmentVariables(IoTDeviceVariablePrefix))
            .UseServiceProviderFactory(
                IoTServiceProviderFactory.ForDevice)
            .ConfigureContainer<IoTDeviceBuilder>(container =>
                configureAction?.Invoke(container));
    }

}
