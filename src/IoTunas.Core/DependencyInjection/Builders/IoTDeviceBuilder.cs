namespace IoTunas.Core.DependencyInjection.Builders;

using IoTunas.Core.ClientBuilders.Device;
using IoTunas.Core.Hosting;
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
        Services.AddSingleton(provider => Client.Build());
        Services.AddHostedService<IoTDeviceHostedService>();
        return Services.BuildServiceProvider();
    }

}
