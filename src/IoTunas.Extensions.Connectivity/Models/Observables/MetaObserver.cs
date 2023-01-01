namespace IoTunas.Extensions.Connectivity.Models.Observables;

using IoTunas.Core.Seedwork;

public class MetaObserver
{

    public InheritedType<IConnectionObserver> Type { get; }

    public MetaObserver(Type type)
    {
        Type = type;
    }

}
