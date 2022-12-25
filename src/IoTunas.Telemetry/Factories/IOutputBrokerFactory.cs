namespace IoTunas.Extensions.Telemetry.Factories;

using IoTunas.Extensions.Telemetry.Models;
using System;
using System.Diagnostics.CodeAnalysis;

public interface IOutputBrokerFactory
{

    public IEnumerable<IOutputBroker> GetAll();

    bool TryGetValue(
        Type type,
        [MaybeNullWhen(false)] out IOutputBroker broker);

    bool TryGetValue<TType>(
        [MaybeNullWhen(false)] out IOutputBroker broker);

}
