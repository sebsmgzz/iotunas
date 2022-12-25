namespace IoTunas.Extensions.Telemetry.Factories;

using IoTunas.Extensions.Telemetry.Models;
using System.Diagnostics.CodeAnalysis;

public interface IReceiverFactory
{

    bool TryGet(
        string inputName,
        [MaybeNullWhen(false)] out IReceiver receiver);

}
