namespace IoTunas.Core.Builders.ModuleClients.Strategies;

using IoTunas.Core.Collections;
using Microsoft.Azure.Devices.Client;

public abstract class ModuleClientBuilderStrategy : IModuleClientBuilderStrategy
{

    protected ClientOptions? options;

    public TransportSettingsList TransportSettings { get; }

    public ClientOptions? Options => options ??= new ClientOptions();

    public ModuleClientBuilderStrategy()
    {
        TransportSettings = new TransportSettingsList();
    }

    public abstract ModuleClient Build();

}
