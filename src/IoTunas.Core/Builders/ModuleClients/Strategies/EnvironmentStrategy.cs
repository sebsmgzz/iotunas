namespace IoTunas.Core.Builders.ModuleClients.Strategies;

using Microsoft.Azure.Devices.Client;
using IoTunas.Core.Builders.ModuleClients;

/// <inheritdoc cref="IModuleClientBuilderStrategy"/>
public class EnvironmentStrategy : IModuleClientBuilderStrategy
{

    public virtual ModuleClient Build(
        ITransportSettings[] transportSettings,
        ClientOptions? clientOptions = null)
    {
        return ModuleClient.CreateFromEnvironmentAsync(
            transportSettings: transportSettings,
            options: clientOptions).Result;
    }

}
