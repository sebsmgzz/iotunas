namespace IoTunas.Core.Builders.DeviceClients;

using IoTunas.Core.Builders.DeviceClients.Strategies;
using Microsoft.Azure.Devices.Client;

public interface IDeviceClientBuilder
{

    DeviceClient Build();
    
    void UseConnectionString(ConnectionStringStrategy strategy);
    
    void UseConnectionString(string connectionString);
    
    void UseGatewayConnection(GatewayConnectionStrategy strategy);
    
    void UseGatewayConnection(string gatewayHostname, string hostName, IAuthenticationMethod authenticationMethod);
    
    void UseHostConnection(HostConnectionStrategy strategy);
    
    void UseHostConnection(string hostName, IAuthenticationMethod authenticationMethod);

}