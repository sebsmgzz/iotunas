namespace IoTunas.Core.Services.ClientBuilders.Modules;

using IoTunas.Core.Services.ClientBuilders.Strategies;
using Microsoft.Azure.Devices.Client;

/// <summary>
/// Represents the strategy used to build a client.
/// </summary>
public interface IModuleClientBuilderStrategy : IClientBuilder
{

    /// <summary>
    /// Builds a client meant to run in an IoT module.
    /// </summary>
    /// <returns>The module client.</returns>
    ModuleClient BuildModuleClient();

}
