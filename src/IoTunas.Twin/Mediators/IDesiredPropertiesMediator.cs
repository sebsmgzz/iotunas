namespace IoTunas.Twin.Mediators;

using Microsoft.Azure.Devices.Shared;
using System.Threading;
using System.Threading.Tasks;

public interface IDesiredPropertiesMediator
{

    Task PullDesiredPropertiesAsync(
        CancellationToken cancellationToken = default);


    Task UpdateDesiredPropertiesAsync(
        TwinCollection desiredProperties, 
        object userContext);

}