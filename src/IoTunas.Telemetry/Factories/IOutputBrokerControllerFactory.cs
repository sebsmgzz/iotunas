namespace IoTunas.Extensions.Telemetry.Factories;

using System;
using System.Diagnostics.CodeAnalysis;
using IoTunas.Extensions.Telemetry.Controllers;

public interface IOutputBrokerControllerFactory
{
    bool TryGet(Type type, [MaybeNullWhen(false)] out IOutputBrokerController controller);
}