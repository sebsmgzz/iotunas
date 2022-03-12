namespace IoTunas.Core.Builders.DeviceClients.Strategies;

using Microsoft.Azure.Devices.Client;
using System;

public class HostConnectionStrategy : IDeviceClientBuilderStrategy
{

    public string? Hostname { get; set; }

    public IAuthenticationMethod? AuthenticationMethod { get; set; }

    public virtual DeviceClient Build(
        ITransportSettings[] transportSettings, 
        ClientOptions? clientOptions)
    {
        return DeviceClient.Create(
            transportSettings: transportSettings,
            options: clientOptions,
            hostname: Hostname ??
                throw new ArgumentNullException(
                    "Hostname cannot be emtpy."),
            authenticationMethod: AuthenticationMethod ??
                throw new ArgumentNullException(
                    "Authentication method cannot be empty."));
    }

}
