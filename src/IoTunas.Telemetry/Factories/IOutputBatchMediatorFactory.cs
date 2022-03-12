namespace IoTunas.Extensions.Telemetry.Factories;

using IoTunas.Extensions.Telemetry.Mediators;
using IoTunas.Extensions.Telemetry.Models;

public interface IOutputBatchMediatorFactory
{

    IOutputBatchMediator GetClient(BatchClientSettings settings);

    IOutputBatchMediator GetClient(string? outputName = null);

    bool TryGetClient(string outputName, out IOutputBatchMediator client);

}
