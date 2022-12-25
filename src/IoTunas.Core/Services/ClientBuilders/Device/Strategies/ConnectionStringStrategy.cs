namespace IoTunas.Core.ClientBuilders.Device.Strategies;

using Microsoft.Azure.Devices.Client;

public class ConnectionStringStrategy : DeviceClientBuilderStrategy
{

    public string? ConnectionString { get; set; }

    public override DeviceClient Build()
    {
        return DeviceClient.CreateFromConnectionString(
            transportSettings: TransportSettings,
            options: options,
            connectionString: ConnectionString!);
    }

}
