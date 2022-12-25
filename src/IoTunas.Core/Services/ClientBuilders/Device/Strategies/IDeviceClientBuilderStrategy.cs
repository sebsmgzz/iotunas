namespace IoTunas.Core.ClientBuilders.Device.Strategies;

using Microsoft.Azure.Devices.Client;

public interface IDeviceClientBuilderStrategy
{

    DeviceClient Build();

}
