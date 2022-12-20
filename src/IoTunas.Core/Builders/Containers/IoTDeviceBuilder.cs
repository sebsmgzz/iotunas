namespace IoTunas.Core.Builders.Containers;

using IoTunas.Core.Builders.DeviceClients;
using IoTunas.Core.Services;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public class IoTDeviceBuilder : IoTContainerBuilderBase
{

    public const string DefaultConnectionStringName = "Default";

    public IDeviceClientBuilder Client { get; }

    public IoTDeviceBuilder(
        HostBuilderContext context,
        IServiceCollection services) : base(
            configuration: context.Configuration,
            environment: context.HostingEnvironment,
            services: services)
    {
        Client = DeviceClientBuilder.FromConnectionString(DefaultConnectionStringName);
    }

    public override IServiceProvider BuildServiceProvider()
    {
        Services.AddSingleton<DeviceClient>(provider => Client.Build());
        Services.AddHostedService<DeviceHostService>();
        return Services.BuildServiceProvider();
    }

}
