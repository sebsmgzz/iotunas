namespace IoTunas.Connectivity.Builders;

using IoTunas.Connectivity.Collections;
using System;

public interface IConnectionServicesBuilder
{

    void AddObservers(Action<IConnectionObserversMapping> configure);

}