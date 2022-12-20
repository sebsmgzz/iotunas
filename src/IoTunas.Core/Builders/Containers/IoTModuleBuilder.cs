namespace IoTunas.Core.Builders.Containers;

using IoTunas.Core.Builders.ModuleClients;
using IoTunas.Core.Services;
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
        Services.AddSingleton<ModuleClient>(provider => Client.Build());
        Services.AddHostedService<ModuleHostService>();
        return Services.BuildServiceProvider();
    }

}
