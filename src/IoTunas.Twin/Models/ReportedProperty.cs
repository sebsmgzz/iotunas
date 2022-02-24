namespace IoTunas.Twin.Models;

using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json.Linq;

public abstract class ReportedProperty : ITwinProperty
{

    public async virtual Task UpdateAsync(TwinCollection twin)
    {
        await Task.Factory.StartNew(() =>
        {
            var jObject = JObject.FromObject(this);
            foreach (var pair in jObject)
            {
                twin[pair.Key] = pair.Value;
            }
        });
    }

}
