namespace IoTunas.Core.Builders.DeviceClients;

using IoTunas.Core.Builders.DeviceClients.Strategies;
using Microsoft.Azure.Devices.Client;

internal class DeviceClientBuilder : IDeviceClientBuilder
{

    private IDeviceClientBuilderStrategy strategy;

    private DeviceClientBuilder(IDeviceClientBuilderStrategy strategy)
    {
        this.strategy = strategy;
    }

    public DeviceClient Build()
    {
        return strategy!.Build();
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

    public static IDeviceClientBuilder FromConnectionString(string connectionString)
    {
        var builder = new DeviceClientBuilder(null);
        builder.UseConnectionString(connectionString);
        return builder;
    }

}