namespace IoTunas.Extensions.Telemetry.Models.Reception;

using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

public abstract class JsonReceiver<T> : ITelemetryReceiver
{

    protected readonly JsonSerializer jsonSerializer;

    public JsonReceiver() : this(new JsonSerializer())
    {
    }

    public JsonReceiver(JsonSerializer jsonSerializer)
    {
        this.jsonSerializer = jsonSerializer;
    }

    public async Task<MessageResponse> HandleAsync(Message message, object userContext)
    {
        using var streamReader = new StreamReader(message.BodyStream);
        using var jsonReader = new JsonTextReader(streamReader);
        var payloadModel = jsonSerializer.Deserialize<T>(jsonReader);
        return await HandleAsync(payloadModel, userContext);
    }

    public abstract Task<MessageResponse> HandleAsync(T model, object userContext);

}
