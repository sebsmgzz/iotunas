namespace IoTunas.Core.Builders.ModuleClients;

using Microsoft.Azure.Devices.Client;

/// <summary>
/// Represents the strategy used to build a module client.
/// </summary>
public interface IModuleClientBuilderStrategy
{

    /// <summary>
    /// Builds the module client.
    /// </summary>
    /// <param name="transportSettings">The transport settings used by the client.</param>
    /// <param name="clientOptions">The client options for the misc settings.</param>
    /// <returns>The module client.</returns>
    ModuleClient Build(
        ITransportSettings[] transportSettings,
        ClientOptions? clientOptions = null);

}
