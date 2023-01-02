namespace IoTunas.Extensions.Methods.Commands;

using Microsoft.Azure.Devices.Client;

public interface ICommandMediator
{

    Task<MethodResponse> HandleAsync(
        MethodRequest methodRequest, object userContext);

}
