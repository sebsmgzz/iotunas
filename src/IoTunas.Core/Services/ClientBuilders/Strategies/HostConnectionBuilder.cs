namespace IoTunas.Core.Services.ClientBuilders.Strategies;

using IoTunas.Core.Services.ClientBuilders.Devices;
using IoTunas.Core.Services.ClientBuilders.Modules;
using Microsoft.Azure.Devices.Client;

public class HostConnectionBuilder : ClientBuilderBase, 
    IDeviceClientBuilderStrategy, IModuleClientBuilderStrategy
{

    /// <summary>
    /// The name of the host.
    /// </summary>
    public string? Hostname { get; set; }

    /// <summary>
    /// The desired authentication method for the iothub.
    /// </summary>
    public IAuthenticationMethod? AuthenticationMethod { get; set; }

    public virtual ModuleClient BuildModuleClient()
    {
        return ModuleClient.Create(
            transportSettings: Transports,
            options: options,
            hostname: Hostname!,
            authenticationMethod: AuthenticationMethod!);

    }

    public virtual DeviceClient BuildDeviceClient()
    {
        return DeviceClient.Create(
            transportSettings: Transports,
            options: options,
            hostname: Hostname!,
            authenticationMethod: AuthenticationMethod!);
    }

}
