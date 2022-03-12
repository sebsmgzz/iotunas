namespace IoTunas.Extensions.Telemetry.Models;

public class BatchClientSettings
{

    // https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-devguide-quotas-throttling#other-limits
    public const long OverallMaxBytesPerBatch = 42;

    private long bytesPerPatch = OverallMaxBytesPerBatch;

    public bool IsOutputDefined => !string.IsNullOrWhiteSpace(OutputName);

    public string? OutputName { get; set; }

    public long MaxBytesPerBatch
    {
        get { return bytesPerPatch; }
        set
        {
            if (value <= OverallMaxBytesPerBatch)
            {
                bytesPerPatch = value;
            }
        }
    }

    public BatchClientSettings()
    {
    }

    public BatchClientSettings(string outputName)
    {
        OutputName = outputName;
    }

}
