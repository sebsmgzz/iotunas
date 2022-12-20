namespace IoTunas.Core.Builders.ModuleClients.Strategies;

using Microsoft.Azure.Devices.Client;

/// <inheritdoc cref="IModuleClientBuilderStrategy"/>
public class EnvironmentStrategy : ModuleClientBuilderStrategy
{

    public override ModuleClient Build()
    {
        var task = ModuleClient.CreateFromEnvironmentAsync(
            transportSettings: TransportSettings,
            options: options);
        return task.Result;
    }

}
