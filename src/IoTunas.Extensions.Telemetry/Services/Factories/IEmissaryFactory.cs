namespace IoTunas.Extensions.Telemetry.Factories;

using IoTunas.Extensions.Telemetry.Models;
using System;
using System.Diagnostics.CodeAnalysis;

public interface IEmissaryFactory
{

    public IEnumerable<IEmissary> GetAll();

    bool TryGetValue(
        Type type,
        [MaybeNullWhen(false)] out IEmissary broker);

    bool TryGetValue<TType>(
        [MaybeNullWhen(false)] out IEmissary broker);

}
