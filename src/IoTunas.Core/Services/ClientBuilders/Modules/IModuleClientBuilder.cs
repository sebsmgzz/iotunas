namespace IoTunas.Core.Services.ClientBuilders.Modules;

using IoTunas.Core.Services.ClientBuilders.Strategies;
using Microsoft.Azure.Devices.Client;

public interface IModuleClientBuilder : IClientBuilder
{

    ModuleClient Build();

    void UseEnvironment();

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
