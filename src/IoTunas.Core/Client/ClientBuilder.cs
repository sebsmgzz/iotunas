namespace IoTunas.Core.Building;

using Microsoft.Azure.Devices.Client;
using IoTunas.Core.Building.Strategies;

/// <inheritdoc cref="IClientBuilder"/>
public class ClientBuilder : IClientBuilder
{

    private IClientBuilderStrategy strategy;
    private ClientOptions? clientOptions;

    public TransportSettingsList TransportSettings { get; }

    public ClientOptions Options => clientOptions ??= new ClientOptions();

    public ClientBuilder(IClientBuilderStrategy strategy)
    {
        this.strategy = strategy;
        TransportSettings = new TransportSettingsList();
    }

    public static ClientBuilder FromEnvironment()
    {
        return new ClientBuilder(new EnvironmentStrategy());
    }

    public void UseEnvironment()
    {
        strategy = new EnvironmentStrategy();
    }

    public void UseConnectionString(string connectionString)
    {
        UseConnectionString(new ConnectionStringStrategy()
        {
            ConnectionString = connectionString
        });
    }

    public void UseConnectionString(ConnectionStringStrategy strategy)
    {
        this.strategy = strategy;
    }

    public void UseHostConnection(string hostName, IAuthenticationMethod authenticationMethod)
    {
        UseHostConnection(new HostConnectionStrategy()
        {
            Hostname = hostName,
            AuthenticationMethod = authenticationMethod
        });
    }

    public void UseHostConnection(HostConnectionStrategy strategy)
    {
        this.strategy = strategy;
    }

    public void UseGatewayConnection(string gatewayHostname, string hostName, IAuthenticationMethod authenticationMethod)
    {
        UseGatewayConnection(new GatewayConnectionStrategy()
        {
            GatewayHostname = gatewayHostname,
            Hostname = hostName,
            AuthenticationMethod = authenticationMethod
        });
    }

    public void UseGatewayConnection(GatewayConnectionStrategy strategy)
    {
        this.strategy = strategy;
    }

    public ModuleClient Build()
    {
        return strategy.Build(
            transportSettings: TransportSettings.ToArray(),
            clientOptions: clientOptions);
    }

}
