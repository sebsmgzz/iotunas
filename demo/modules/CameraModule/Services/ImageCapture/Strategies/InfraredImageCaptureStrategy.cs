namespace CameraModule.Services.ImageCapture.Strategies;

using CameraModule.Services.ImageCapture;
using System;

public class InfraredImageCaptureStrategy : IImageCaptureStrategy
{

    private static readonly Random random = new();

    public byte[] Capture()
    {
        // TODO: Invoke call to infrared hardaware to capture image
        var noise = new byte[50 * 50 * 3]; // 50px * 50px
        random.NextBytes(noise);
        return noise;
    }

}