namespace IoTunas.Core.Services.ClientBuilders.Devices;

using IoTunas.Core.Collections;
using IoTunas.Core.Services.ClientBuilders.Strategies;
using Microsoft.Azure.Devices.Client;

internal class DeviceClientBuilder : IDeviceClientBuilder
{

    private IDeviceClientBuilderStrategy strategy;

    public ClientOptions Options => strategy.Options;

    public TransportSettingsList Transports => strategy.Transports;

    public DeviceClientBuilder() : this(new EmptyBuilder())
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
        strategy = new ConnectionStringBuilder()
        {
            ConnectionString = connectionString
        };
    }

    public void UseHostConnection(string hostName, IAuthenticationMethod authenticationMethod)
    {
        strategy = new HostConnectionBuilder()
        {
            Hostname = hostName,
            AuthenticationMethod = authenticationMethod
        };
    }

    public void UseGatewayConnection(string gatewayHostname, string hostName, IAuthenticationMethod authenticationMethod)
    {
        strategy = new GatewayConnectionBuilder()
        {
            GatewayHostname = gatewayHostname,
            Hostname = hostName,
            AuthenticationMethod = authenticationMethod
        };
    }

}