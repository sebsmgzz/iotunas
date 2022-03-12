namespace IoTunas.Core.Builders;

using IoTunas.Core.Collections;
using Microsoft.Azure.Devices.Client;

public class ClientBuilderBase : IClientBuilderBase
{

    protected readonly Lazy<ClientOptions> clientOptions;

    public TransportSettingsList TransportSettings { get; }

    public ClientOptions Options => clientOptions.Value;

    public ClientBuilderBase()
    {
        TransportSettings = new TransportSettingsList();
        clientOptions = new(() => new ClientOptions());
    }

}
