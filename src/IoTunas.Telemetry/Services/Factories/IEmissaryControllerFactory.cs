namespace IoTunas.Extensions.Telemetry.Factories;

using System;
using System.Diagnostics.CodeAnalysis;
using IoTunas.Extensions.Telemetry.Controllers;

public interface IEmissaryControllerFactory
{
    
    bool TryGet(
        Type type, 
        [MaybeNullWhen(false)] out IEmissaryController controller);

    IEnumerable<IEmissaryController> GetAll();

}
