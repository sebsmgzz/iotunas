namespace IoTunas.Core.Services.ClientBuilders.Strategies;

using IoTunas.Core.Services.ClientBuilders.Devices;
using IoTunas.Core.Services.ClientBuilders.Modules;
using Microsoft.Azure.Devices.Client;

public class ConnectionStringStrategy : ClientBuilderStrategyBase, 
    IDeviceClientBuilderStrategy, IModuleClientBuilderStrategy
{

    /// <summary>
    /// The connection string to be used by the module client to establish connectivity.
    /// </summary>
    public string? ConnectionString { get; set; }

    public virtual DeviceClient BuildDeviceClient()
    {
        return DeviceClient.CreateFromConnectionString(
            transportSettings: TransportSettings,
            options: options,
            connectionString: ConnectionString!);
    }

    public virtual ModuleClient BuildModuleClient()
    {
        return ModuleClient.CreateFromConnectionString(
            transportSettings: TransportSettings,
            options: options,
            connectionString: ConnectionString!);
    }

}
