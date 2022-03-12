namespace SpeakerModule.Commands;

using System;

public class FindMeCommandPayload
{

    public bool Force { get; set; } = false;

    public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(10);

}
