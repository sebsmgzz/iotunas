namespace IoTunas.Extensions.Methods.Models;

using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Threading.Tasks;

public abstract class Command<T> : ICommand
{

    public async Task<MethodResponse> HandleAsync(
        MethodRequest methodRequest, object userContext)
    {
        using var dataStream = new MemoryStream(methodRequest.Data);
        using var streamReader = new StreamReader(dataStream);
        using var jsonReader = new JsonTextReader(streamReader);
        var serializer = new JsonSerializer();
        var payloadModel = serializer.Deserialize<T>(jsonReader);
        return await HandleAsync(payloadModel, userContext);
    }

    public abstract Task<MethodResponse> HandleAsync(
        T payloadModel, object userContext);

}
