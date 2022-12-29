namespace IoTunas.Demos.GuidsModule.Services.Guids;

using System;

public class GuidGenerator : ISingletonGuidGenerator, IScopedGuidGenerator, ITransientGuidGenerator
{

    public Guid Value { get; }

    public GuidGenerator()
    {
        Value = Guid.NewGuid();
    }

}
