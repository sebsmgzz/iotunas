namespace IoTunas.Core.Services.ClientBuilders.Strategies;

using IoTunas.Core.Services.ClientBuilders.Modules;
using Microsoft.Azure.Devices.Client;

public class EnvironmentStrategy : ClientBuilderStrategyBase, IModuleClientBuilderStrategy
{

    public virtual ModuleClient BuildModuleClient()
    {
        var task = ModuleClient.CreateFromEnvironmentAsync(
            transportSettings: TransportSettings,
            options: options);
        return task.Result;
    }

}
