namespace IoTunas.Core.Services.ClientHosts.Devices;

using Microsoft.Azure.Devices.Client;
using System;

public interface IIoTDeviceHost : IIoTClientHost, IDisposable, IAsyncDisposable
{

    DeviceClient Client { get; }

}
