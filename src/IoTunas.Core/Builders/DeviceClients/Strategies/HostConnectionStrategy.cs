namespace IoTunas.Core.Builders.DeviceClients.Strategies;

using Microsoft.Azure.Devices.Client;

public class HostConnectionStrategy : DeviceClientBuilderStrategy
{

    public string? Hostname { get; set; }

    public IAuthenticationMethod? AuthenticationMethod { get; set; }

    public override DeviceClient Build()
    {
        return DeviceClient.Create(
            transportSettings: TransportSettings,
            options: Options,
            hostname: Hostname!,
            authenticationMethod: AuthenticationMethod!);
    }

}
