namespace IoTunas.Core.DependencyInjection.Devices;

using IoTunas.Core.DependencyInjection;
using IoTunas.Core.Services.ClientBuilders.Devices;

public interface IIoTDeviceBuilder : IIoTBuilder
{
    
    IDeviceClientBuilder Client { get; }

}
