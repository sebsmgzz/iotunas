namespace IoTunas.Core.ClientBuilders.Device.Strategies;

using IoTunas.Core.Collections;
using Microsoft.Azure.Devices.Client;

public abstract class DeviceClientBuilderStrategy : IDeviceClientBuilderStrategy
{

    protected ClientOptions? options;

    public TransportSettingsList TransportSettings { get; }

    public ClientOptions? Options => options ??= new ClientOptions();

    public DeviceClientBuilderStrategy()
    {
        TransportSettings = new TransportSettingsList();
    }

    public abstract DeviceClient Build();

}