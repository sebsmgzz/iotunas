namespace IoTunas.Core.DependencyInjection.Modules;

using IoTunas.Core.DependencyInjection;
using IoTunas.Core.Services.ClientBuilders.Modules;
using IoTunas.Core.Services.ClientHosts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public class IoTModuleBuilder : IoTContainerBuilderBase, IIoTModuleBuilder
{

    public IModuleClientBuilder Client { get; }

    public IoTModuleBuilder(
        HostBuilderContext context,
        IServiceCollection services) 
        : base(context, services)
    {
        Client = new ModuleClientBuilder();
    }

    public override IServiceProvider BuildServiceProvider()
    {
        Services.AddSingleton(provider => Client.Build());
        Services.AddHostedService<IoTModuleHost>();
        return Services.BuildServiceProvider();
    }

}
