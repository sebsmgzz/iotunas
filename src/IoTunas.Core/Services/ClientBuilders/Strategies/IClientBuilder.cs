namespace IoTunas.Core.Services.ClientBuilders.Strategies;

using IoTunas.Core.Collections;
using Microsoft.Azure.Devices.Client;

public interface IClientBuilder
{
    
    ClientOptions Options { get; }
    
    TransportSettingsList Transports { get; }

}