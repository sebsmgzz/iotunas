namespace IoTunas.Extensions.Commands.Mediators;

using Microsoft.Azure.Devices.Client;

public interface ICommandHandlerMediator
{

    Task<MethodResponse> HandleAsync(
        MethodRequest methodRequest, object userContext);

}
