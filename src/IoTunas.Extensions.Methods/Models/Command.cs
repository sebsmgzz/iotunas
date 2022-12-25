namespace IoTunas.Extensions.Methods.Models;

using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Threading.Tasks;

public abstract class Command<T> : ICommand
{

    public async Task<MethodResponse> HandleAsync(
        MethodRequest methodRequest, object userContext)
    {
        var payloadModel = JsonConvert.DeserializeObject<T>(methodRequest.DataAsJson);
        return await HandleAsync(payloadModel, userContext);
    }

    public abstract Task<MethodResponse> HandleAsync(
        T payloadModel, object userContext);

}
