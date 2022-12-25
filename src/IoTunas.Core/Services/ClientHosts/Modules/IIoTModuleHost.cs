namespace IoTunas.Core.Services.ClientHosts.Modules;

using Microsoft.Azure.Devices.Client;
using System;

public interface IIoTModuleHost : IIoTClientHost, IDisposable, IAsyncDisposable
{

    ModuleClient Client { get; }

}
