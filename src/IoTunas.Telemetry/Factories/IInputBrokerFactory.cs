namespace IoTunas.Extensions.Telemetry.Factories;

using IoTunas.Extensions.Telemetry.Models;
using System.Diagnostics.CodeAnalysis;

public interface IInputBrokerFactory
{

    bool TryGet(
        string inputName,
        [MaybeNullWhen(false)]
        out IInputBroker broker);

}
