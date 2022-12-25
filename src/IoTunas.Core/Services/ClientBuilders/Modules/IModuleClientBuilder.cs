namespace IoTunas.Core.Services.ClientBuilders.Modules;

using Microsoft.Azure.Devices.Client;

public interface IModuleClientBuilder
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
