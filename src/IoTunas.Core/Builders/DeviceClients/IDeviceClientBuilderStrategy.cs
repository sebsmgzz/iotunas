namespace IoTunas.Core.Builders.DeviceClients;

using Microsoft.Azure.Devices.Client;

public interface IDeviceClientBuilderStrategy
{

    DeviceClient Build(
        ITransportSettings[] transportSettings, 
        ClientOptions? clientOptions);

}
