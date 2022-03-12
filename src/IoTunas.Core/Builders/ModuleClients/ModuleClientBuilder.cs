namespace IoTunas.Core.Builders.ModuleClients;

using Microsoft.Azure.Devices.Client;
using IoTunas.Core.Builders.ModuleClients.Strategies;

internal class ModuleClientBuilder : ClientBuilderBase, IModuleClientBuilder
{

    private IModuleClientBuilderStrategy strategy;

    public ModuleClientBuilder(IModuleClientBuilderStrategy strategy)
    {
        this.strategy = strategy;
    }

    public static ModuleClientBuilder FromEnvironment()
    {
        return new ModuleClientBuilder(new EnvironmentStrategy());
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
        return strategy!.Build(
            transportSettings: TransportSettings.ToArray(),
            clientOptions: clientOptions.IsValueCreated ?
                clientOptions.Value : null);
    }

}
