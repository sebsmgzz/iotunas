namespace IoTunas.Core.Services.ClientBuilders.Strategies;

using Microsoft.Azure.Devices.Client;

public class GatewayConnectionStrategy : HostConnectionStrategy
{

    /// <summary>
    /// The name of the gateway hostname
    /// </summary>
    public string? GatewayHostname { get; set; }

    public override ModuleClient BuildModuleClient()
    {
        return ModuleClient.Create(
            transportSettings: TransportSettings,
            options: options,
            hostname: Hostname!,
            authenticationMethod: AuthenticationMethod!,
            gatewayHostname: GatewayHostname!);
    }

}
