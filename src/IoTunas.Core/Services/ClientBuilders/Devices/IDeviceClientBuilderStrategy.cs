namespace IoTunas.Core.Services.ClientBuilders.Devices;

using IoTunas.Core.Services.ClientBuilders.Strategies;
using Microsoft.Azure.Devices.Client;

/// <summary>
/// Represents the strategy used to build a client.
/// </summary>
public interface IDeviceClientBuilderStrategy : IClientBuilder
{

    /// <summary>
    /// Builds a client meant to run in an IoT device.
    /// </summary>
    /// <returns>The device client.</returns>
    DeviceClient BuildDeviceClient();

}
