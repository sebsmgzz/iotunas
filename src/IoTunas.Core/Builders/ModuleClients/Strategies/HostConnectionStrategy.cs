namespace IoTunas.Core.Builders.ModuleClients.Strategies;

using Microsoft.Azure.Devices.Client;
using IoTunas.Core.Builders.ModuleClients;

/// <inheritdoc cref="IModuleClientBuilderStrategy"/>
public class HostConnectionStrategy : IModuleClientBuilderStrategy
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
            hostname: Hostname!,
            authenticationMethod: AuthenticationMethod!);

    }

}
