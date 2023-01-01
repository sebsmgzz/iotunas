namespace IoTunas.Extensions.Telemetry.Reception;

using IoTunas.Extensions.Telemetry.Models.Reception;
using System.Diagnostics.CodeAnalysis;

public interface IReceiverFactory
{

    bool TryGet(
        string inputName,
        [MaybeNullWhen(false)] out ITelemetryReceiver receiver);

}
