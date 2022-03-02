namespace SpeakerModule.Services;

using Microsoft.Extensions.Logging;
using SpeakerModule.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

public class FindMeService : IFindMeService
{

    private readonly ILogger logger;
    private Task currentTask;
    private DateTime? lastInvokation;

    public bool IsRunning => currentTask?.IsCompleted ?? true;

    public TimeSpan CompletesIn
    {
        get
        {
            if (IsRunning && lastInvokation != null)
            {
                return DateTime.UtcNow - lastInvokation.Value;
            }
            return TimeSpan.Zero;
        }
    }

    public FindMeService(ILogger<IFindMeService> logger)
    {
        this.logger = logger;
    }

    public async Task StartAsync(FindMeOptions options = null)
    {
        options ??= FindMeOptions.Defaults();
        logger.LogInformation($"Starting new {nameof(IFindMeService)}");
        currentTask = Task.Factory.StartNew(() =>
        {
            lastInvokation = DateTime.UtcNow;
            // TODO: Make call to hardware so the speaker emits a loud noise
            // Code in here, may vary depending on the implementation
            // of the call to the hardware
            Thread.Sleep(options.Duration);
        });
        await Task.CompletedTask;
    }

}
