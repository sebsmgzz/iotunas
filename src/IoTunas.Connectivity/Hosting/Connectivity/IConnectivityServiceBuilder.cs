namespace IoTunas.Extensions.Connectivity.Hosting.Connectivity;

using IoTunas.Extensions.Connectivity.Collections;

public interface IConnectivityServiceBuilder
{

    ConnectionObserverServiceCollection Observers { get; }

}
