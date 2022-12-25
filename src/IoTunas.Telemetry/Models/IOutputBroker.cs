namespace IoTunas.Extensions.Telemetry.Models;

using Microsoft.Azure.Devices.Client;

public interface IOutputBroker
{

    Task<Message[]> HandleAsync(
        CancellationToken cancellationToken = default);

}
