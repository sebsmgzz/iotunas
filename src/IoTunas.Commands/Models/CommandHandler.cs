namespace IoTunas.Commands.Models;

using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Threading.Tasks;

public abstract class CommandHandler<T> : ICommandHandler
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
