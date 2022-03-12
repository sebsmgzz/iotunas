namespace IoTunas.Extensions.Telemetry.Mediators;

using IoTunas.Extensions.Telemetry.Models;
using Microsoft.Azure.Devices.Client;
using System.Threading.Tasks;

public interface IOutputBatchMediator
{

    BatchClientSettings Settings { get; }

    int Count { get; }

    void Add(params Message[] messages);

    bool TryAdd(params Message[] messages);

    void Remove(params Message[] messages);

    void Clear();

    Task SendAsync(CancellationToken cancellationToken = default);

}
