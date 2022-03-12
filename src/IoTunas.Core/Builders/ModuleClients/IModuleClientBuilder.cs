namespace IoTunas.Core.Builders.ModuleClients;

using IoTunas.Core.Builders.ModuleClients.Strategies;
using Microsoft.Azure.Devices.Client;

public interface IModuleClientBuilder : IClientBuilderBase
{

    ModuleClient Build();

    void UseConnectionString(ConnectionStringStrategy strategy);

    void UseConnectionString(string connectionString);

    void UseEnvironment();

    void UseGatewayConnection(GatewayConnectionStrategy strategy);

    void UseGatewayConnection(string gatewayHostname, string hostName, IAuthenticationMethod authenticationMethod);

    void UseHostConnection(HostConnectionStrategy strategy);

    void UseHostConnection(string hostName, IAuthenticationMethod authenticationMethod);

}