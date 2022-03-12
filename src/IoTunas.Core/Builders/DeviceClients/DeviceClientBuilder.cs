namespace IoTunas.Core.Builders.DeviceClients;

using IoTunas.Core.Builders.DeviceClients.Strategies;
using Microsoft.Azure.Devices.Client;

internal class DeviceClientBuilder : ClientBuilderBase, IDeviceClientBuilder
{

    private IDeviceClientBuilderStrategy strategy;

    public DeviceClientBuilder(IDeviceClientBuilderStrategy strategy)
    {
        this.strategy = strategy;
    }

    public static DeviceClientBuilder FromConnectionString(string connectionString)
    {
        return new DeviceClientBuilder(new ConnectionStringStrategy()
        {
            ConnectionString = connectionString
        });
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

    public DeviceClient Build()
    {
        return strategy!.Build(
            transportSettings: TransportSettings.ToArray(),
            clientOptions: clientOptions.IsValueCreated ?
                clientOptions.Value : null);
    }

}