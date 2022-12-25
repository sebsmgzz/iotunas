namespace IoTunas.Core.Services.ClientBuilders.Devices;

using Microsoft.Azure.Devices.Client;

/// <summary>
/// Represents the strategy used to build a client.
/// </summary>
public interface IDeviceClientBuilderStrategy
{

    /// <summary>
    /// Builds a client meant to run in an IoT device.
    /// </summary>
    /// <returns>The device client.</returns>
    DeviceClient BuildDeviceClient();

}
