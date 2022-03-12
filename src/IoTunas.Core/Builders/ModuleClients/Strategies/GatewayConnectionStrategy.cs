namespace IoTunas.Core.Builders.ModuleClients.Strategies;

using IoTunas.Core.Builders.ModuleClients;
using Microsoft.Azure.Devices.Client;

/// <inheritdoc cref="IModuleClientBuilderStrategy"/>
public class GatewayConnectionStrategy : HostConnectionStrategy
{

    /// <summary>
    /// The name of the gateway hostname
    /// </summary>
    public string? GatewayHostname { get; set; }

    public override ModuleClient Build(
        ITransportSettings[] transportSettings,
        ClientOptions? clientOptions = null)
    {
        return ModuleClient.Create(
            transportSettings: transportSettings,
            options: clientOptions,
            hostname: Hostname ??
                throw new ArgumentNullException(
                    "Hostname cannot be emtpy."),
            authenticationMethod: AuthenticationMethod ??
                throw new ArgumentNullException(
                    "Authentication method cannot be empty."),
            gatewayHostname: GatewayHostname ??
                throw new ArgumentNullException(
                    "Gateway hostname cannot be empty."));
    }

}
