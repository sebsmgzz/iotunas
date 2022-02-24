namespace IoTunas.Twin.Models;

using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;

public abstract class DesiredProperty : ITwinProperty
{

    public async virtual Task UpdateAsync(TwinCollection twin)
    {
        await Task.Factory.StartNew(() =>
            JsonConvert.PopulateObject(twin.ToJson(), this));
    }

}
