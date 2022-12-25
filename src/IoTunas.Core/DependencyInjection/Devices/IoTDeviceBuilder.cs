namespace IoTunas.Core.DependencyInjection.Devices;

using IoTunas.Core.DependencyInjection;
using IoTunas.Core.Services.ClientBuilders.Devices;
using IoTunas.Core.Services.ClientHosts;
using IoTunas.Core.Services.ClientHosts.Devices;
using Microsoft.Azure.Devices.Client;
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
        Services.AddSingleton<IIoTDeviceHost, IoTDeviceHost>();
        Services.AddTransient<IIoTClientHost>(p => p.GetRequiredService<IIoTDeviceHost>());
        Services.AddHostedService<IIoTDeviceHost>(p => p.GetRequiredService<IIoTDeviceHost>());
        Services.AddTransient<DeviceClient>(p => p.GetRequiredService<IIoTDeviceHost>().Client);
        return Services.BuildServiceProvider();
    }

}
