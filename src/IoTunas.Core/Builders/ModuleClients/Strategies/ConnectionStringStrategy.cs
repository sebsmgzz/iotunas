namespace IoTunas.Core.Builders.ModuleClients.Strategies;

using Microsoft.Azure.Devices.Client;
using IoTunas.Core.Builders.ModuleClients;

/// <inheritdoc cref="IModuleClientBuilderStrategy"/>
public class ConnectionStringStrategy : IModuleClientBuilderStrategy
{

    /// <summary>
    /// The connection string to be used by the module client to establish connectivity.
    /// </summary>
    public string? ConnectionString { get; set; }

    public virtual ModuleClient Build(
        ITransportSettings[] transportSettings,
        ClientOptions? clientOptions = null)
    {
        return ModuleClient.CreateFromConnectionString(
            transportSettings: transportSettings,
            options: clientOptions,
            connectionString: ConnectionString!);
    }

}
