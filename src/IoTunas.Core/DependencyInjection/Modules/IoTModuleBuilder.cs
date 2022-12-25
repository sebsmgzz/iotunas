namespace IoTunas.Core.DependencyInjection.Modules;

using IoTunas.Core.DependencyInjection;
using IoTunas.Core.Services.ClientBuilders.Modules;
using IoTunas.Core.Services.ClientHosts;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using IoTunas.Core.Services.ClientHosts.Modules;

public class IoTModuleBuilder : IoTBuilderBase, IIoTModuleBuilder
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
        Services.AddSingleton<IIoTModuleHost, IoTModuleHost>();
        Services.AddTransient<IIoTClientHost>(p => p.GetRequiredService<IIoTModuleHost>());
        Services.AddHostedService<IIoTModuleHost>(p => p.GetRequiredService<IIoTModuleHost>());
        Services.AddTransient<ModuleClient>(p => p.GetRequiredService<IIoTModuleHost>().Client);
        return Services.BuildServiceProvider();
    }

}
