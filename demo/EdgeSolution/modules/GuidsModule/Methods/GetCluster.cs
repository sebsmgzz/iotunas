namespace IoTunas.Demos.GuidsModule.Methods;

using IoTunas.Demos.GuidsModule.Services.Guids;
using IoTunas.Extensions.Methods.Models;
using IoTunas.Extensions.Methods.Services.Factories;
using Microsoft.Azure.Devices.Client;
using System.Threading.Tasks;

public class GetClusters : Command<GetClusterPayload>
{

    private readonly IMethodResponseFactory responses;
    private readonly IGuidProvider provider;

    public GetClusters(
        IMethodResponseFactory responses,
        IGuidProvider provider)
    {
        this.responses = responses;
        this.provider = provider;
    }

    public override Task<MethodResponse> HandleAsync(
        GetClusterPayload payload, 
        object userContext)
    {
        var response = responses.Ok(new
        {
            clusters = GetClusterEnumerable(payload.Size)
        });
        return Task.FromResult(response);
    }

    private IEnumerable<GuidCluster> GetClusterEnumerable(int size)
    {
        for (int i = 0; i < size; i++)
        {
            yield return provider.AsCluster();
        }
    }

}
