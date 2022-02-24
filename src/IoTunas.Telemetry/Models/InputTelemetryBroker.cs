namespace IoTunas.Telemetry.Models;

using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Threading.Tasks;

public abstract class InputTelemetryBroker<T> : IInputTelemetryBroker
{

    public async Task<MessageResponse> ReceiveAsync(
        Message message, object userContext)
    {
        var reader = new StreamReader(message.BodyStream);
        var telemetryModel = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
        return await ReceiveAsync(telemetryModel, userContext);
    }

    public abstract Task<MessageResponse> ReceiveAsync(
        T telemetryModel, object userContext);

}
