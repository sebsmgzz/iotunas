namespace IoTunas.Core.ClientBuilders.Module;

using Microsoft.Azure.Devices.Client;
using IoTunas.Core.ClientBuilders.Module.Strategies;

internal class ModuleClientBuilder : IModuleClientBuilder
{

    private IModuleClientBuilderStrategy strategy;

    private ModuleClientBuilder(IModuleClientBuilderStrategy strategy)
    {
        this.strategy = strategy;
    }

    public ModuleClient Build()
    {
        return strategy.Build();
    }

    public void UseEnvironment()
    {
        strategy = new EnvironmentStrategy();
    }

    public void UseConnectionString(string connectionString)
    {
        strategy = new ConnectionStringStrategy()
        {
            ConnectionString = connectionString
        };
    }

    public void UseHostConnection(
        string hostName, 
        IAuthenticationMethod authenticationMethod)
    {
        strategy = new HostConnectionStrategy()
        {
            Hostname = hostName,
            AuthenticationMethod = authenticationMethod
        };
    }

    public void UseGatewayConnection(
        string gatewayHostname, 
        string hostName, 
        IAuthenticationMethod authenticationMethod)
    {
        strategy = new GatewayConnectionStrategy()
        {
            GatewayHostname = gatewayHostname,
            Hostname = hostName,
            AuthenticationMethod = authenticationMethod
        };
    }

    public static ModuleClientBuilder FromEnvironment()
    {
        var client = new ModuleClientBuilder(null);
        client.UseEnvironment();
        return client;
    }

    public static ModuleClientBuilder FromConnectionString(string connectionString)
    {
        var client = new ModuleClientBuilder(null);
        client.UseConnectionString(connectionString);
        return client;
    }

}
