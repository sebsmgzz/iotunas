namespace CameraModule.Services.ImageCapture;

using System.Threading;
using System.Threading.Tasks;

public interface IImageCaptureService
{

    Task CaptureImageAsync(CancellationToken cancellationToken = default);

    Task<bool> SetInfraredAsync(CancellationToken cancellationToken = default);

    Task<bool> SetNormalAsync(CancellationToken cancellationToken = default);

}
