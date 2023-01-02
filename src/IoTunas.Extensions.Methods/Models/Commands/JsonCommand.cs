namespace IoTunas.Extensions.Methods.Models.Commands;

using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Threading.Tasks;

public abstract class JsonCommand<T> : ICommand
{

    protected readonly JsonSerializer jsonSerializer;

    public JsonCommand() : this(new JsonSerializer())
    {
    }

    public JsonCommand(JsonSerializer jsonSerializer)
    {
        this.jsonSerializer = jsonSerializer;
    }

    public async Task<MethodResponse> HandleAsync(MethodRequest methodRequest, object userContext)
    {
        using var dataStream = new MemoryStream(methodRequest.Data);
        using var streamReader = new StreamReader(dataStream);
        using var jsonReader = new JsonTextReader(streamReader);
        var payloadModel = jsonSerializer.Deserialize<T>(jsonReader);
        return await HandleAsync(payloadModel, userContext);
    }

    public abstract Task<MethodResponse> HandleAsync(T payloadModel, object userContext);

}
