namespace IoTunas.Extensions.Telemetry.Emission;

using IoTunas.Extensions.Telemetry.Models.Emission;
using System.Threading;

public interface ITelemetrySender
{

    Task<bool> SendBatchAsync<TTelemetry>(
        IEnumerable<TTelemetry> telemetry,
        string? outputName = null,
        CancellationToken cancellationToken = default)
        where TTelemetry : ITelemetry;

    Task<bool> SendAsync<TTelemetry>(
        TTelemetry telemetry,
        string? outputName = null,
        CancellationToken cancellationToken = default)
        where TTelemetry : ITelemetry;

    Task<bool> SendAsync(
        Type providerType,
        Action<ITelemetryProvider<ITelemetry>>? action = null,
        CancellationToken cancellationToken = default);

    Task<bool> SendAsync<TProvider>(
        Action<TProvider>? action = null,
        CancellationToken cancellationToken = default)
        where TProvider : class, ITelemetryProvider<ITelemetry>;

}
