namespace CameraModule.Services.ImageCapture;

using CameraModule.Models;
using CameraModule.Services.ImageCapture.Strategies;
using CameraModule.Twin;
using IoTunas.Extensions.Telemetry.Mediators;
using IoTunas.Extensions.Twin.Mediators;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public class ImageCaptureService : IImageCaptureService
{

    private readonly InfraredDetails infraredDetails;
    private readonly IOutputTelemetryMediator telemetry;
    private readonly IReportedPropertiesMediator reportedProperties;
    private readonly ILogger logger;
    private IImageCaptureStrategy strategy;

    public ImageCaptureService(
        InfraredDetails infraredDetails,
        IOutputTelemetryMediator telemetry,
        IReportedPropertiesMediator reportedProperties,
        ILogger<IImageCaptureService> logger)
    {
        this.infraredDetails = infraredDetails;
        this.telemetry = telemetry;
        this.reportedProperties = reportedProperties;
        this.logger = logger;
        strategy = new NormalImageCaptureStrategy();
    }

    public async Task CaptureImageAsync(CancellationToken cancellationToken = default)
    {
        var message = new ImageCapture()
        {
            CaptureTime = DateTime.UtcNow,
            Value = strategy.Capture()
        };
        await telemetry
            .SendAsync(message, cancellation: cancellationToken)
            .ContinueWith(t => logger.LogInformation(
                $"Sent 1 image captured at {message.CaptureTime}"), cancellationToken);
    }

    public async Task<bool> SetInfraredAsync(CancellationToken cancellationToken = default)
    {
        var isSet = strategy is InfraredImageCaptureStrategy;
        if (!isSet)
        {
            strategy = new InfraredImageCaptureStrategy();
            infraredDetails.Status = InfraredStatus.On;
            await reportedProperties.PushReportedPropertiesAsync(cancellationToken);
        }
        return isSet;
    }

    public async Task<bool> SetNormalAsync(CancellationToken cancellationToken = default)
    {
        var isSet = strategy is NormalImageCaptureStrategy;
        if (!isSet)
        {
            strategy = new NormalImageCaptureStrategy();
            infraredDetails.Status = InfraredStatus.Off;
            await reportedProperties.PushReportedPropertiesAsync(cancellationToken);
        }
        return isSet;
    }

}
