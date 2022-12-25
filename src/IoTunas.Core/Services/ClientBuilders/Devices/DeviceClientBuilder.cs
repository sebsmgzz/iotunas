namespace IoTunas.Core.Services.ClientBuilders.Devices;

using IoTunas.Core.Services.ClientBuilders.Strategies;
using Microsoft.Azure.Devices.Client;

internal class DeviceClientBuilder : IDeviceClientBuilder
{

    private IDeviceClientBuilderStrategy strategy;

    public DeviceClientBuilder() : this(new EmptyStrategy())
    {
    }

    public DeviceClientBuilder(IDeviceClientBuilderStrategy strategy)
    {
        this.strategy = strategy;
    }

    public DeviceClient Build()
    {
        return strategy!.BuildDeviceClient();
    }

    public void UseConnectionString(string connectionString)
    {
        strategy = new ConnectionStringStrategy()
        {
            ConnectionString = connectionString
        };
    }

    public void UseHostConnection(string hostName, IAuthenticationMethod authenticationMethod)
    {
        strategy = new HostConnectionStrategy()
        {
            Hostname = hostName,
            AuthenticationMethod = authenticationMethod
        };
    }

    public void UseGatewayConnection(string gatewayHostname, string hostName, IAuthenticationMethod authenticationMethod)
    {
        strategy = new GatewayConnectionStrategy()
        {
            GatewayHostname = gatewayHostname,
            Hostname = hostName,
            AuthenticationMethod = authenticationMethod
        };
    }

}