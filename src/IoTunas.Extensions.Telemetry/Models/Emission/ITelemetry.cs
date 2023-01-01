namespace IoTunas.Extensions.Telemetry.Models.Emission;

using Microsoft.Azure.Devices.Client;

public interface ITelemetry
{

    Message AsMessage();

}
