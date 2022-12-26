namespace IoTunas.Extensions.Twin.Services.Mediators;

using IoTunas.Extensions.Twin.Models;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class DesiredTwinMediator : IDesiredTwinMediator
{

    private readonly IDesiredTwinModel twinModel;

    public DesiredTwinMediator(IDesiredTwinModel twinModel)
    {
        this.twinModel = twinModel;
    }

    public Task HandlePropertyUpdate(TwinCollection desiredProperties, object userContext)
    {
        var update = desiredProperties.ToJson();
        JsonConvert.PopulateObject(update, twinModel);
        return Task.CompletedTask;
    }

}
