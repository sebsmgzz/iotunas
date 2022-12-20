namespace IoTunas.Core.Builders.DeviceClients.Strategies;

using Microsoft.Azure.Devices.Client;

public class GatewayConnectionStrategy : HostConnectionStrategy
{

    public string? GatewayHostname { get; set; }

    public override DeviceClient Build()
    {
        return DeviceClient.Create(
            transportSettings: TransportSettings,
            options: Options,
            hostname: Hostname!,
            authenticationMethod: AuthenticationMethod!,
            gatewayHostname: GatewayHostname!);
    }

}