namespace IoTunas.Extensions.Telemetry.Mediators;

using IoTunas.Extensions.Telemetry.Factories;
using IoTunas.Extensions.Telemetry.Models;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

public class OutputBatchTelemetryMediator : IOutputBatchMediator
{

    private readonly ModuleClient moduleClient;
    private readonly ILogger logger;
    private readonly List<Message> batch;

    public BatchClientSettings Settings { get; }

    public int Count => batch.Count;

    public OutputBatchTelemetryMediator(
        ModuleClient moduleClient,
        IMessageFactory messageFactory,
        ILogger<IOutputBatchMediator> logger,
        BatchClientSettings settings)
    {
        this.moduleClient = moduleClient;
        this.logger = logger;
        batch = new List<Message>();
        Settings = settings;
    }

    public void Add(params Message[] messages)
    {
        batch.AddRange(messages);
        var bytesCount = batch.Sum(message => message.GetBytes().Length);
        if (bytesCount > Settings.MaxBytesPerBatch)
        {
            throw new InvalidOperationException(
                $"Maximum byte size for this batch is {Settings.MaxBytesPerBatch}." +
                $"Maximum byte size for any batch is {BatchClientSettings.OverallMaxBytesPerBatch}");
        }
    }

    public bool TryAdd(params Message[] messages)
    {
        var bytesCount = batch.Sum(message => message.GetBytes().Length);
        foreach (var message in messages)
        {
            bytesCount += message.GetBytes().Length;
            if (bytesCount > Settings.MaxBytesPerBatch)
            {
                return false;
            }
        }
        batch.AddRange(messages);
        return true;
    }

    public void Remove(params Message[] messages)
    {
        foreach (var message in messages)
        {
            batch.Remove(message);
        }
    }

    public void Clear()
    {
        batch.Clear();
    }

    public async Task SendAsync(CancellationToken cancellationToken = default)
    {
        if (Settings.IsOutputDefined)
        {
            logger.LogInformation($"Sending {batch.Count} messages in batch to {Settings.OutputName}.");
            await moduleClient.SendEventBatchAsync(Settings.OutputName, batch, cancellationToken);
        }
        else
        {
            logger.LogInformation($"Sending {batch.Count} messages in batch.");
            await moduleClient.SendEventBatchAsync(batch, cancellationToken);
        }
    }

}