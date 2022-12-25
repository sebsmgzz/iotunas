namespace IoTunas.Extensions.Telemetry.Models;

using Microsoft.Azure.Devices.Client;

public interface IEmissary
{

    Task<Message[]> HandleAsync(
        CancellationToken cancellationToken = default);

}
