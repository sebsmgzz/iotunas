namespace IoTunas.Demos.GuidsModule.Twin;

using IoTunas.Extensions.Twin.Models;
using Newtonsoft.Json;

public class DesiredTwin : IDesiredTwinModel
{

    [JsonProperty("clusterPeriod")]
    public TimeSpan ClusterPeriod { get; }

    [JsonProperty("clusterSize")]
    public int ClusterSize{ get; }

}
