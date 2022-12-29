namespace IoTunas.Demos.GuidsModule.Services.Guids;

public class GuidProvider : IGuidProvider
{

    private readonly IServiceProvider provider;

    public GuidProvider(IServiceProvider provider)
    {
        this.provider = provider;
    }

    public GuidCluster AsCluster()
    {
        var singleton = provider.GetRequiredService<ISingletonGuidGenerator>();
        var scoped = provider.GetRequiredService<IScopedGuidGenerator>();
        var transient = provider.GetRequiredService<ITransientGuidGenerator>();
        return new GuidCluster(
            singleton.Value,
            scoped.Value,
            transient.Value);
    }

}
