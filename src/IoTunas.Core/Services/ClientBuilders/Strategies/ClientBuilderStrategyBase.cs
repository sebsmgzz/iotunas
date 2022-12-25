namespace IoTunas.Core.Services.ClientBuilders.Strategies;

using IoTunas.Core.Collections;
using Microsoft.Azure.Devices.Client;

public abstract class ClientBuilderStrategyBase
{

    protected ClientOptions? options;

    public TransportSettingsList TransportSettings { get; }

    public ClientOptions? Options => options ??= new ClientOptions();

    public ClientBuilderStrategyBase()
    {
        TransportSettings = new TransportSettingsList();
    }

}
