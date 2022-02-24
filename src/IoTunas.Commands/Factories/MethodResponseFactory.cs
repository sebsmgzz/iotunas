namespace IoTunas.Commands.Factories;

using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;

public class MethodResponseFactory : IMethodResponseFactory
{

    public MethodResponse Ok(object? payload = null)
    {
        return Respond(200, payload);
    }

    public MethodResponse BadRequest(object? payload = null)
    {
        return Respond(400, payload);
    }

    public MethodResponse NotFound(object? payload = null)
    {
        return Respond(404, payload);
    }

    public MethodResponse BrewCoffe(object? payload = null)
    {
        return Respond(418, payload);
    }

    public MethodResponse InternalError(object? payload = null)
    {
        return Respond(500, payload);
    }

    private static MethodResponse Respond(int statusCode, object? payload = null)
    {
        var json = JsonConvert.SerializeObject(payload);
        var jsonBytes = Encoding.ASCII.GetBytes(json);
        return new MethodResponse(jsonBytes, statusCode);
    }

}