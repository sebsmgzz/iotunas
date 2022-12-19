namespace IoTunas.Core.Builders.DeviceClients.Strategies;

using Microsoft.Azure.Devices.Client;

public class HostConnectionStrategy : IDeviceClientBuilderStrategy
{

    public string? Hostname { get; set; }

    public IAuthenticationMethod? AuthenticationMethod { get; set; }

    public virtual DeviceClient Build(
        ITransportSettings[] transportSettings, 
        ClientOptions? clientOptions)
    {
        return DeviceClient.Create(
            transportSettings: transportSettings,
            options: clientOptions,
            hostname: Hostname!,
            authenticationMethod: AuthenticationMethod!);
    }

}
