namespace IoTunas.Core.ClientBuilders.Module.Strategies;

using Microsoft.Azure.Devices.Client;

/// <summary>
/// Represents the strategy used to build a module client.
/// </summary>
public interface IModuleClientBuilderStrategy
{

    /// <summary>
    /// Builds the module client.
    /// </summary>
    /// <returns>The module client.</returns>
    ModuleClient Build();

}
