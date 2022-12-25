namespace IoTunas.Core.ClientBuilders.Module.Strategies;

using Microsoft.Azure.Devices.Client;

/// <inheritdoc cref="IModuleClientBuilderStrategy"/>
public class HostConnectionStrategy : ModuleClientBuilderStrategy
{

    /// <summary>
    /// The name of the host.
    /// </summary>
    public string? Hostname { get; set; }

    /// <summary>
    /// The desired authentication method for the iothub.
    /// </summary>
    public IAuthenticationMethod? AuthenticationMethod { get; set; }

    public override ModuleClient Build()
    {
        return ModuleClient.Create(
            transportSettings: TransportSettings,
            options: options,
            hostname: Hostname!,
            authenticationMethod: AuthenticationMethod!);

    }

}
