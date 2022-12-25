namespace IoTunas.Core.Services.ClientBuilders.Modules;

using IoTunas.Core.Services.ClientBuilders.Strategies;
using Microsoft.Azure.Devices.Client;

internal class ModuleClientBuilder : IModuleClientBuilder
{

    private IModuleClientBuilderStrategy strategy;

    public ModuleClientBuilder() : this(new EmptyStrategy())
    {
    }

    public ModuleClientBuilder(IModuleClientBuilderStrategy strategy)
    {
        this.strategy = strategy;
    }

    public ModuleClient Build()
    {
        return strategy.BuildModuleClient();
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

}
