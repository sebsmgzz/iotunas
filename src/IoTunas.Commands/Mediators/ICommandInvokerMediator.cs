namespace IoTunas.Commands.Mediators;

using Microsoft.Azure.Devices.Client;

public interface ICommandInvokerMediator
{

    Task<MethodResponse> InvokeAsync(
        string deviceId,
        MethodRequest request,
        CancellationToken cancellationToken = default);

    Task<MethodResponse> InvokeAsync(
        string deviceId,
        string moduleId,
        MethodRequest request,
        CancellationToken cancellationToken = default);

}
