namespace IoTunas.Core.Builders.DeviceClients.Strategies;

using Microsoft.Azure.Devices.Client;
using System;

public class ConnectionStringStrategy : IDeviceClientBuilderStrategy
{

    public string? ConnectionString { get; set; }

    public virtual DeviceClient Build(
        ITransportSettings[] transportSettings, 
        ClientOptions? clientOptions)
    {
        return DeviceClient.CreateFromConnectionString(
            transportSettings: transportSettings,
            options: clientOptions,
            connectionString: ConnectionString ??
                throw new ArgumentNullException(
                    "Connection string cannot be empty."));
    }

}
