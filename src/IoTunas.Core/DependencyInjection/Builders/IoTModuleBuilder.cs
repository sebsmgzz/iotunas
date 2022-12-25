namespace IoTunas.Core.DependencyInjection.Builders;

using IoTunas.Core.ClientBuilders.Module;
using IoTunas.Core.Hosting;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public class IoTModuleBuilder : IoTContainerBuilderBase
{

    public IModuleClientBuilder Client { get; }

    public IoTModuleBuilder(
        HostBuilderContext context,
        IServiceCollection services) : base(
            configuration: context.Configuration,
            environment: context.HostingEnvironment,
            services: services)
    {
        Client = ModuleClientBuilder.FromEnvironment();
    }

    public override IServiceProvider BuildServiceProvider()
    {
        Services.AddSingleton(provider => Client.Build());
        Services.AddHostedService<IoTModuleHostedService>();
        return Services.BuildServiceProvider();
    }

}
