namespace SpeakerModule.Commands;

using IoTunas.Commands.Factories;
using IoTunas.Commands.Models;
using IoTunas.Commands.Reflection;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SpeakerModule.Models;
using SpeakerModule.Services;
using System.Text;
using System.Threading.Tasks;

[CommandName("neoFindMe")]
public class ExecuteNeoFindMeCommand : CommandHandler<FindMeCommandPayload>
{

    private readonly IFindMeService findMeService;
    private readonly IMethodResponseFactory responseFactory;
    private readonly ILogger logger;

    public ExecuteNeoFindMeCommand(
        IFindMeService findMeService,
        IMethodResponseFactory responseFactory,
        ILogger<ExecuteNeoFindMeCommand> logger)
    {
        this.findMeService = findMeService;
        this.responseFactory = responseFactory;
        this.logger = logger;
    }

    public override async Task<MethodResponse> HandleAsync(
        FindMeCommandPayload payload, object userContext)
    {
        var options = Payload2Options(payload);
        if (!findMeService.IsRunning || payload.Force)
        {
            await findMeService.StartAsync(options);
        }
        else
        {
            logger.LogWarning($"Ignoring call to {nameof(ExecuteFindMeCommand)} since " +
                $"{nameof(IFindMeService)} is already running. " +
                $"Try again in {findMeService.CompletesIn.TotalSeconds} seconds " +
                $"or force invoke the call.");
        }
        var statusCode = findMeService.IsRunning ? 200 : 409;
        var response = JsonConvert.SerializeObject(new
        {
            isRunning = findMeService.IsRunning,
            completesIn = findMeService.CompletesIn.TotalSeconds
        });
        return new MethodResponse(Encoding.ASCII.GetBytes(response), statusCode);
    }

    private FindMeOptions Payload2Options(FindMeCommandPayload payload)
    {
        return new FindMeOptions()
        {
            Duration = payload.Duration
        };
    }

}
