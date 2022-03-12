namespace IoTunas.Extensions.Telemetry.Models;

using System.Collections.Generic;

public class BatchClientFactorySettings
{

    public Dictionary<string, BatchClientSettings> BatchClientSettings { get; set; } = new();

}
