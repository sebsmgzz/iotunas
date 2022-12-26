namespace IoTunas.Core.Services.ClientBuilders.Modules;

using IoTunas.Core.Collections;
using IoTunas.Core.Services.ClientBuilders.Strategies;
using Microsoft.Azure.Devices.Client;

internal class ModuleClientBuilder : IModuleClientBuilder
{

    private IModuleClientBuilderStrategy strategy;

    public ClientOptions Options => strategy.Options;

    public TransportSettingsList Transports => strategy.Transports;

    public ModuleClientBuilder() : this(new EmptyBuilder())
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
        strategy = new EnvironmentBuilder();
    }

    public void UseConnectionString(string connectionString)
    {
        strategy = new ConnectionStringBuilder()
        {
            ConnectionString = connectionString
        };
    }

    public void UseHostConnection(
        string hostName, 
        IAuthenticationMethod authenticationMethod)
    {
        strategy = new HostConnectionBuilder()
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
        strategy = new GatewayConnectionBuilder()
        {
            GatewayHostname = gatewayHostname,
            Hostname = hostName,
            AuthenticationMethod = authenticationMethod
        };
    }

}
