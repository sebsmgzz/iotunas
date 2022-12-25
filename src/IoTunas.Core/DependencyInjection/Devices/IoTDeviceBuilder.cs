namespace IoTunas.Core.DependencyInjection.Devices;

using IoTunas.Core.DependencyInjection;
using IoTunas.Core.Services.ClientBuilders.Devices;
using IoTunas.Core.Services.ClientHosts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public class IoTDeviceBuilder : IoTContainerBuilderBase, IIoTDeviceBuilder
{

    public IDeviceClientBuilder Client { get; }

    public IoTDeviceBuilder(
        HostBuilderContext context,
        IServiceCollection services) 
        : base(context, services)
    {
        Client = new DeviceClientBuilder();
    }

    public override IServiceProvider BuildServiceProvider()
    {
        Services.AddSingleton(provider => Client.Build());
        Services.AddHostedService<IoTDeviceHost>();
        return Services.BuildServiceProvider();
    }

}
