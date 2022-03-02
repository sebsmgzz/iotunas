namespace SpeakerModule.Services;

using SpeakerModule.Models;
using System;
using System.Threading.Tasks;

public interface IFindMeService
{

    bool IsRunning { get; }

    TimeSpan CompletesIn { get; }

    Task StartAsync(FindMeOptions options = null);

}
