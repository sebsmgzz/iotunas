namespace IoTunas.Core.Builders.DeviceClients.Strategies;

using Microsoft.Azure.Devices.Client;

public interface IDeviceClientBuilderStrategy
{

    DeviceClient Build();

}
