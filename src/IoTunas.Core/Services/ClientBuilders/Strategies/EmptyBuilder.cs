namespace IoTunas.Core.Services.ClientBuilders.Strategies;

using IoTunas.Core.Services.ClientBuilders.Devices;
using IoTunas.Core.Services.ClientBuilders.Modules;
using Microsoft.Azure.Devices.Client;

public class EmptyBuilder : ClientBuilderBase, IDeviceClientBuilderStrategy, IModuleClientBuilderStrategy
{

    public DeviceClient BuildDeviceClient()
    {
        throw new NotImplementedException();
    }

    public ModuleClient BuildModuleClient()
    {
        throw new NotImplementedException();
    }

}
