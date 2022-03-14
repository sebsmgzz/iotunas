namespace CameraModule.Twin;

using IoTunas.Extensions.Twin.Models;
using IoTunas.Extensions.Twin.Reflection;
using Newtonsoft.Json;

[TwinPropertyName("infraredDetails")]
public class InfraredDetails : ReportedProperty
{

    [JsonProperty("status")]
    public InfraredStatus Status { get; set; } = InfraredStatus.Off;

    [JsonProperty("resolution")]
    public double Resolution { get; set; } = 42.23;

}
