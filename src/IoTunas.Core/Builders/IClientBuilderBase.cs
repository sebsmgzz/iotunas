namespace IoTunas.Core.Builders;

using IoTunas.Core.Collections;
using Microsoft.Azure.Devices.Client;

public interface IClientBuilderBase
{
    ClientOptions Options { get; }
    TransportSettingsList TransportSettings { get; }
}