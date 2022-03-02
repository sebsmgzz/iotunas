namespace SpeakerModule.Models;

using System;

public class FindMeOptions
{

    public TimeSpan Duration { get; set; }

    public static FindMeOptions Defaults()
    {
        return new()
        {
            Duration = TimeSpan.FromSeconds(10)
        };
    }

}
