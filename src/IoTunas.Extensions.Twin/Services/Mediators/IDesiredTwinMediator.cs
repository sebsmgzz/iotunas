namespace IoTunas.Extensions.Twin.Services.Mediators;
using Microsoft.Azure.Devices.Shared;
using System.Threading.Tasks;

public interface IDesiredTwinMediator
{

    Task HandlePropertyUpdate(
        TwinCollection desiredProperties,
        object userContext);

}
