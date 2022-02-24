namespace IoTunas.Core.Building.Strategies;

using Microsoft.Azure.Devices.Client;
using IoTunas.Core.Building;

/// <inheritdoc cref="IClientBuilderStrategy"/>
public class ConnectionStringStrategy : IClientBuilderStrategy
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
            connectionString: ConnectionString ??
                throw new ArgumentNullException(
                    "Connection string cannot be empty."));
    }

}
