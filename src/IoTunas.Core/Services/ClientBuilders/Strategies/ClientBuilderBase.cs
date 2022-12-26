namespace IoTunas.Core.Services.ClientBuilders.Strategies;

using IoTunas.Core.Collections;
using Microsoft.Azure.Devices.Client;

public abstract class ClientBuilderBase : IClientBuilder
{

    protected ClientOptions? options;

    public TransportSettingsList Transports { get; }

    public ClientOptions Options => options ??= new ClientOptions();

    public ClientBuilderBase()
    {
        Transports = new TransportSettingsList();
    }

}
