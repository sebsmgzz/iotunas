namespace IoTunas.Extensions.Twin.Models;

using Microsoft.Azure.Devices.Shared;

public interface ITwinProperty
{

    Task UpdateAsync(TwinCollection twin);

}
