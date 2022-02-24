namespace IoTunas.Core.Building;

using Microsoft.Azure.Devices.Client;
using System;
using IoTunas.Core.Building.Strategies;

/// <summary>
/// Responsible for instantiating a module client given and initial configuration.
/// </summary>
public interface IClientBuilder
{

    /// <summary>
    /// The transport settings to be used by the client.
    /// </summary>
    TransportSettingsList TransportSettings { get; }

    /// <summary>
    /// The misc client options.
    /// </summary>
    ClientOptions Options { get; }

    /// <summary>
    /// Configures the builder to build the client from the environment.
    /// Use this for production.
    /// </summary>
    void UseEnvironment();

    /// <summary>
    /// Configures the builder to build the client using a connection string.
    /// Use this for development only.
    /// </summary>
    /// <param name="connectionString">The connection string to be used.</param>
    void UseConnectionString(string connectionString);

    /// <summary>
    /// Configures the builder to build the client using a connection string.
    /// Use this for development only.
    /// </summary>
    /// <param name="strategy">The connection string strategy to be used.</param>
    void UseConnectionString(ConnectionStringStrategy strategy);

    /// <summary>
    /// Configures the builder to build the client as a gateway.
    /// </summary>
    /// <param name="gatewayHostname">The fullly qualified name of the gateway host.</param>
    /// <param name="hostName">The name of the host.</param>
    /// <param name="authenticationMethod">The authentication method used with the iothub.</param>
    void UseGatewayConnection(string gatewayHostname, string hostName, IAuthenticationMethod authenticationMethod);

    /// <summary>
    /// Configures the builder to build the client as a gateway.
    /// </summary>
    /// <param name="strategy">The gateway connection strategy to be used.</param>
    void UseGatewayConnection(GatewayConnectionStrategy strategy);

    /// <summary>
    /// Configures the builder to build the client as a raw host.
    /// </summary>
    /// <param name="hostName">The name of the host.</param>
    /// <param name="authenticationMethod">The authentication method used with the iothub.</param>
    void UseHostConnection(string hostName, IAuthenticationMethod authenticationMethod);

    /// <summary>
    /// Configures the builder to build the client as a raw host.
    /// </summary>
    /// <param name="strategy">The connection strategy to be used.</param>
    void UseHostConnection(HostConnectionStrategy strategy);

    /// <summary>
    /// Builds the module client using the current strategy.
    /// </summary>
    /// <returns>The new module client.</returns>
    ModuleClient Build();

}