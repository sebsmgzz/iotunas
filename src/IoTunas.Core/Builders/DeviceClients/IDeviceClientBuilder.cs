namespace IoTunas.Core.Builders.DeviceClients;

using Microsoft.Azure.Devices.Client;

public interface IDeviceClientBuilder
{

    DeviceClient Build();
    
    void UseConnectionString(
        string connectionString);
    
    void UseGatewayConnection(
        string gatewayHostname, 
        string hostName, 
        IAuthenticationMethod authenticationMethod);
    
    void UseHostConnection(
        string hostName, 
        IAuthenticationMethod authenticationMethod);

}
