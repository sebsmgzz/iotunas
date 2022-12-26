namespace IoTunas.Core.Services.ClientBuilders.Devices;

using IoTunas.Core.Services.ClientBuilders.Strategies;
using Microsoft.Azure.Devices.Client;

public interface IDeviceClientBuilder : IClientBuilder
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
