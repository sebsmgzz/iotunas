namespace IoTunas.Extensions.Telemetry.Models;

using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

public abstract class Receiver<T> : IReceiver
{

    public async Task<MessageResponse> HandleAsync(Message message, object userContext)
    {
        using var streamReader = new StreamReader(message.BodyStream);
        using var jsonReader = new JsonTextReader(streamReader);
        var serializer = new JsonSerializer();
        var payloadModel = serializer.Deserialize<T>(jsonReader);
        return await HandleAsync(payloadModel, userContext);
    }

    public abstract Task<MessageResponse> HandleAsync(T? model, object userContext);


}