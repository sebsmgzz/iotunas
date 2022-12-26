namespace IoTunas.Core.Services.ClientBuilders.Strategies;

using IoTunas.Core.Services.ClientBuilders.Modules;
using Microsoft.Azure.Devices.Client;

public class EnvironmentBuilder : ClientBuilderBase, IModuleClientBuilderStrategy
{

    public virtual ModuleClient BuildModuleClient()
    {
        var task = ModuleClient.CreateFromEnvironmentAsync(
            transportSettings: Transports,
            options: options);
        return task.Result;
    }

}
