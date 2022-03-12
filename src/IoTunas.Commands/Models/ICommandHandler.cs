namespace IoTunas.Extensions.Commands.Models;

using Microsoft.Azure.Devices.Client;
using System.Threading.Tasks;

public interface ICommandHandler
{

    Task<MethodResponse> HandleAsync(
        MethodRequest methodRequest, object userContext);

}
