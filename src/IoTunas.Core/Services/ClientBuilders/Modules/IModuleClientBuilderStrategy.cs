namespace IoTunas.Core.Services.ClientBuilders.Modules;

using Microsoft.Azure.Devices.Client;

/// <summary>
/// Represents the strategy used to build a client.
/// </summary>
public interface IModuleClientBuilderStrategy
{

    /// <summary>
    /// Builds a client meant to run in an IoT module.
    /// </summary>
    /// <returns>The module client.</returns>
    ModuleClient BuildModuleClient();

}
