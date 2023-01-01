namespace IoTunas.Extensions.Telemetry.Models.Emission;

using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;

public abstract class JsonTelemetry : ITelemetry
{

    private readonly JsonSerializerSettings? settings;

    protected JsonTelemetry(JsonSerializerSettings? settings = null)
    {
        this.settings = settings;
    }

    public string AsJson()
    {
        return settings != null ?
            JsonConvert.SerializeObject(this, settings) :
            JsonConvert.SerializeObject(this);
    }

    public Message AsMessage()
    {
        var json = AsJson();
        var bytes = Encoding.ASCII.GetBytes(json);
        return new Message(bytes);
    }

}
