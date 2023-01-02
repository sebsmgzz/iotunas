namespace IoTunas.Demos.Template.Methods;

using System.Threading.Tasks;
using IoTunas.Demos.Template.Services;
using IoTunas.Extensions.Methods.Commands;
using IoTunas.Extensions.Methods.Models.Commands;
using Microsoft.Azure.Devices.Client;

public class ResetCommand : ICommand
{

    private readonly ICounterService counter;
    private readonly IMethodResponseFactory responses;

    public ResetCommand(
        ICounterService counter,
        IMethodResponseFactory responses)
    {
        this.counter = counter;
        this.responses = responses;
    }

    public Task<MethodResponse> HandleAsync(
        MethodRequest methodRequest, 
        object userContext)
    {
        counter.Reset();
        return Task.FromResult(responses.Ok());
    }

}
