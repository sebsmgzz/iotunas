namespace IoTunas.Extensions.Twin.Mediators;

using IoTunas.Extensions.Twin.Factories;
using IoTunas.Extensions.Twin.Models;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Extensions.Logging;
using System;

public class ReportedPropertiesMediator : IReportedPropertiesMediator
{

    private readonly ModuleClient client;
    private readonly IPropertyFactory<ReportedProperty> factory;
    private readonly ILogger logger;

    private TwinCollection? cachedProperties;

    public ReportedPropertiesMediator(
        ModuleClient client,
        IPropertyFactory<ReportedProperty> factory,
        ILogger<IReportedPropertiesMediator> logger)
    {
        this.client = client;
        this.factory = factory;
        this.logger = logger;
    }

    public async Task PushReportedPropertiesAsync(
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation($"Invoking desired properties update");
        foreach (var propertyName in factory.PropertyNames)
        {
            await PushReportedPropertyAsync(propertyName, cancellationToken);
        }
    }

    public async Task PushReportedPropertyAsync(
        string propertyName,
        CancellationToken cancellationToken = default)
    {

        if(!factory.TryGet(propertyName, out var propertyModel))
        {
            logger.LogCritical($"Skipping | " +
                $"Reported property {propertyName} model needs to " +
                $"inherit {nameof(ReportedProperty)} and be registered " +
                $"in the DI system to be pushed.");
            return;
        }

        if(cachedProperties == null)
        {
            var twin = await client.GetTwinAsync(cancellationToken);
            cachedProperties = twin.Properties.Reported;
        }
        else
        {
            await propertyModel.UpdateAsync(cachedProperties);
        }

        await client.UpdateReportedPropertiesAsync(
            cachedProperties, cancellationToken);

    }

}
