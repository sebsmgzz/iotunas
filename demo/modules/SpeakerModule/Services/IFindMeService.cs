namespace SpeakerModule.Services;

using System;
using System.Threading.Tasks;

public interface IFindMeService
{

    bool IsRunning { get; }

    TimeSpan CompletesIn { get; }

    Task StartAsync(TimeSpan duration);

}
