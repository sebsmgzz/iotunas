namespace IoTunas.Core.Builders.ModuleClients.Strategies;

using Microsoft.Azure.Devices.Client;

/// <inheritdoc cref="IModuleClientBuilderStrategy"/>
public class GatewayConnectionStrategy : HostConnectionStrategy
{

    /// <summary>
    /// The name of the gateway hostname
    /// </summary>
    public string? GatewayHostname { get; set; }

    public override ModuleClient Build()
    {
        return ModuleClient.Create(
            transportSettings: TransportSettings,
            options: options,
            hostname: Hostname!,
            authenticationMethod: AuthenticationMethod!,
            gatewayHostname: GatewayHostname!);
    }

}
