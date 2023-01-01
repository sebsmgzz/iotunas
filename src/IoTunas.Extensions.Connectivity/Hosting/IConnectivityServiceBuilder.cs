namespace IoTunas.Extensions.Connectivity.Hosting;

using IoTunas.Extensions.Connectivity.Collections;

public interface IConnectivityServiceBuilder
{

    IMetaObserverCollection Observers { get; }

}
