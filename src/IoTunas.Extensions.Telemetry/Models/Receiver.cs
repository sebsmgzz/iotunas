namespace IoTunas.Extensions.Telemetry.Models;

using Microsoft.Azure.Devices.Client;
using System.Text.Json;

public abstract class Receiver<T> : IReceiver
{

    public async Task<MessageResponse> HandleAsync(Message message, object userContext)
    {
        var payloadModel = JsonSerializer.Deserialize<T>(message.BodyStream);
        return await HandleAsync(payloadModel, userContext);
    }

    public abstract Task<MessageResponse> HandleAsync(T? model, object userContext);


}