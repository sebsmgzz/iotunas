namespace IoTunas.Telemetry.Factories;

using IoTunas.Telemetry.Mediators;
using IoTunas.Telemetry.Models;

public interface IOutputBatchMediatorFactory
{

    IOutputBatchMediator GetClient(BatchClientSettings settings);

    IOutputBatchMediator GetClient(string? outputName = null);

    bool TryGetClient(string outputName, out IOutputBatchMediator client);

}
