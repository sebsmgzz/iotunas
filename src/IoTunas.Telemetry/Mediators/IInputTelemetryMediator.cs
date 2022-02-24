namespace IoTunas.Telemetry.Mediators;

using Microsoft.Azure.Devices.Client;

public interface IInputTelemetryMediator
{

    Task<MessageResponse> ReceiveAsync(Message message, object userContext);

    Task CompleteAsync(string lockToken, CancellationToken cancellationToken = default);

    Task CompleteAsync(Message message, CancellationToken cancellationToken = default);

    Task AbandonAsync(string lockToken, CancellationToken cancellationToken = default);

    Task AbandonAsync(Message message, CancellationToken cancellationToken = default);

}
