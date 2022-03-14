namespace CameraModule.Services;

using CameraModule.Services.ImageCapture;
using IoTunas.Extensions.Commands.Factories;
using IoTunas.Extensions.Commands.Models;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public class TurnInfraredOnHandler : ICommandHandler
{

    private readonly IImageCaptureService imageCapture;
    private readonly IMethodResponseFactory responseFactory;
    private readonly ILogger logger;

    public TurnInfraredOnHandler(
        IImageCaptureService imageCapture,
        IMethodResponseFactory responseFactory,
        ILogger<TurnInfraredOnHandler> logger)
    {
        this.imageCapture = imageCapture;
        this.responseFactory = responseFactory;
        this.logger = logger;
    }

    public async Task<MethodResponse> HandleAsync(MethodRequest methodRequest, object userContext)
    {
        var infraredWasSet = await imageCapture.SetInfraredAsync();
        if (infraredWasSet)
        {
            logger.LogInformation("Infrared image capture stategy properly set.");
            return responseFactory.Ok();
        }
        else
        {
            logger.LogInformation("Infrared image capture stategy is alredy set.");
            return responseFactory.Conflict();
        }
    }

}
