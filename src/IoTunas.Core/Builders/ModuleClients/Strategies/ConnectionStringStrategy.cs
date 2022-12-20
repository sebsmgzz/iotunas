namespace IoTunas.Core.Builders.ModuleClients.Strategies;

using Microsoft.Azure.Devices.Client;

/// <inheritdoc cref="IModuleClientBuilderStrategy"/>
public class ConnectionStringStrategy : ModuleClientBuilderStrategy
{

    /// <summary>
    /// The connection string to be used by the module client to establish connectivity.
    /// </summary>
    public string? ConnectionString { get; set; }

    public override ModuleClient Build()
    {
        return ModuleClient.CreateFromConnectionString(
            transportSettings: TransportSettings,
            options: options,
            connectionString: ConnectionString!);
    }

}
