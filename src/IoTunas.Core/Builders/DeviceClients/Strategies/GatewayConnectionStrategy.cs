namespace IoTunas.Core.Builders.DeviceClients.Strategies;

using Microsoft.Azure.Devices.Client;
using System;

public class GatewayConnectionStrategy : HostConnectionStrategy
{

    public string? GatewayHostname { get; set; }

    public override DeviceClient Build(
        ITransportSettings[] transportSettings,
        ClientOptions? clientOptions = null)
    {
        return DeviceClient.Create(
            transportSettings: transportSettings,
            options: clientOptions,
            hostname: Hostname!,
            authenticationMethod: AuthenticationMethod!,
            gatewayHostname: GatewayHostname!);
    }

}