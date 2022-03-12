namespace SpeakerModule.Commands;

using IoTunas.Extensions.Commands.Factories;
using IoTunas.Extensions.Commands.Models;
using IoTunas.Extensions.Commands.Reflection;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;
using SpeakerModule.Services;
using System.Threading.Tasks;

[CommandName("findMe")]
public class ExecuteFindMeCommand : CommandHandler<FindMeCommandPayload>
{

    private readonly IFindMeService findMeService;
    private readonly IMethodResponseFactory responseFactory;
    private readonly ILogger logger;

    public ExecuteFindMeCommand(
        IFindMeService findMeService,
        IMethodResponseFactory responseFactory,
        ILogger<ExecuteFindMeCommand> logger)
    {
        this.findMeService = findMeService;
        this.responseFactory = responseFactory;
        this.logger = logger;
    }

    public async override Task<MethodResponse> HandleAsync(
        FindMeCommandPayload payload, object userContext)
    {
        if (!findMeService.IsRunning || payload.Force)
        {
            await findMeService.StartAsync(payload.Duration);
        }
        else
        {
            logger.LogWarning(
                $"Ignoring call to {nameof(ExecuteFindMeCommand)} since " +
                $"{nameof(IFindMeService)} is already running. " +
                $"Try again in {findMeService.CompletesIn.TotalSeconds} seconds " +
                $"or force invoke the call.");
        }
        var response = new
        {
            isRunning = findMeService.IsRunning,
            completesIn = findMeService.CompletesIn.TotalSeconds
        };
        return findMeService.IsRunning ?
            responseFactory.Ok(response) :
            responseFactory.Conflict(response);
    }

}
