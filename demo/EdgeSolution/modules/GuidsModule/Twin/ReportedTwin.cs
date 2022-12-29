namespace IoTunas.Demos.GuidsModule.Twin;

using IoTunas.Extensions.Twin.Models;
using Newtonsoft.Json;

public class ReportedTwin : ReportedTwinModel
{

    private TimeSpan clusterPeriod;
    private int clusterSize;

    [JsonProperty("clusterPeriod")]
    public TimeSpan ClusterPeriod
    {
        get => clusterPeriod;
        set => SetProperty(ref clusterPeriod, value);
    }

    [JsonProperty("clusterSize")]
    public int ClusterSize
    {
        get => clusterSize;
        set => SetProperty(ref clusterSize, value);
    }

}