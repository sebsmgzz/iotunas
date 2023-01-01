namespace IoTunas.Extensions.Telemetry.Models.Reception;

using Microsoft.Azure.Devices.Client;

public interface ITelemetryReceiver
{

    Task<MessageResponse> HandleAsync(
        Message message,
        object userContext);

}
