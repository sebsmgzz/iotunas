namespace IoTunas.Core.Building.Strategies;

using Microsoft.Azure.Devices.Client;
using IoTunas.Core.Building;

/// <inheritdoc cref="IClientBuilderStrategy"/>
public class EnvironmentStrategy : IClientBuilderStrategy
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
