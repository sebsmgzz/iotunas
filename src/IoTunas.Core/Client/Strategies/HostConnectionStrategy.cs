namespace IoTunas.Core.Building.Strategies;

using Microsoft.Azure.Devices.Client;
using IoTunas.Core.Building;

/// <inheritdoc cref="IClientBuilderStrategy"/>
public class HostConnectionStrategy : IClientBuilderStrategy
{

    /// <summary>
    /// The name of the host.
    /// </summary>
    public string? Hostname { get; set; }

    /// <summary>
    /// The desired authentication method for the iothub.
    /// </summary>
    public IAuthenticationMethod? AuthenticationMethod { get; set; }

    public virtual ModuleClient Build(
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
                    "Authentication method cannot be empty."));

    }

}
