namespace IoTunas.Demos.GuidsModule.Services.Guids;

using System;

public class GuidCluster
{

    public Guid Singleton { get; }

    public Guid Scoped { get; }

    public Guid Transient { get; }


    public GuidCluster(Guid singleton, Guid scoped, Guid transient)
    {
        Singleton = singleton;
        Scoped = scoped;
        Transient = transient;
    }

}