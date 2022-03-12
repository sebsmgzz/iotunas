namespace IoTunas.Extensions.Telemetry.Factories;

using IoTunas.Extensions.Telemetry.Mediators;
using IoTunas.Extensions.Telemetry.Models;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class OutputBatchMediatorFactory : IOutputBatchMediatorFactory
{

    private readonly IServiceProvider serviceProvider;
    private readonly BatchClientFactorySettings settings;

    public OutputBatchMediatorFactory(
        IServiceProvider serviceProvider,
        IOptions<BatchClientFactorySettings> options)
    {
        this.serviceProvider = serviceProvider;
        settings = options.Value;
    }

    public IOutputBatchMediator GetClient(BatchClientSettings settings)
    {
        return new OutputBatchTelemetryMediator(
            serviceProvider.GetRequiredService<ModuleClient>(),
            serviceProvider.GetRequiredService<IMessageFactory>(),
            serviceProvider.GetRequiredService<ILogger<IOutputBatchMediator>>(),
            settings);
    }

    public IOutputBatchMediator GetClient(string? outputName = null)
    {
        if (outputName != null && TryGetClient(outputName, out var client))
        {
            return client;
        }
        return GetClient(new BatchClientSettings());
    }

    public bool TryGetClient(string outputName, out IOutputBatchMediator client)
    {
        if (settings.BatchClientSettings
            .TryGetValue(outputName, out var clientSettings))
        {
            client = GetClient(clientSettings);
            return true;
        }
        client = null;
        return false;
    }

}
