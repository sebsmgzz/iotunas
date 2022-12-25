namespace IoTunas.Extensions.Commands.Services.Mediators;

using Microsoft.Azure.Devices.Client;

public interface ICommandMediator
{

    Task<MethodResponse> HandleAsync(
        MethodRequest methodRequest, object userContext);

}
