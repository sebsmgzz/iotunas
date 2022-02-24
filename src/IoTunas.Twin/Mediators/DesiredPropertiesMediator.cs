namespace IoTunas.Twin.Mediators;

using IoTunas.Twin.Factories;
using IoTunas.Twin.Models;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Extensions.Logging;
using System;

public class DesiredPropertiesMediator : IDesiredPropertiesMediator
{

    private readonly ModuleClient client;
    private readonly IPropertyFactory<DesiredProperty> factory;
    private readonly ILogger logger;

    public DesiredPropertiesMediator(
        ModuleClient client,
        IPropertyFactory<DesiredProperty> factory,
        ILogger<IReportedPropertiesMediator> logger)
    {
        this.client = client;
        this.factory = factory;
        this.logger = logger;
    }

    public async Task PullDesiredPropertiesAsync(
        CancellationToken cancellationToken = default)
    {
        var twin = await client.GetTwinAsync(cancellationToken);
        await UpdateDesiredPropertiesAsync(
            twin.Properties.Desired, this, cancellationToken);
    }

    private async Task UpdateDesiredPropertiesAsync(
        TwinCollection desiredProperties, object userContext, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Invoking desired properties update");
        foreach (var propertyName in factory.PropertyNames)
        {

            // Skip properties not included in the update
            if (!desiredProperties.Contains(propertyName))
            {
                logger.LogWarning($"Skipping | {propertyName}");
                continue;
            }

            // If the property model does not inherit the base class
            // or it is not registered in the DI service provider
            // raise an exception because this should NEVER happen
            if (!factory.TryGet(propertyName, out var propertyModel))
            {
                logger.LogCritical($"Skipping | " +
                    $"Desired property {propertyName} model needs to " +
                    $"inherit {nameof(DesiredProperty)} and be registered " +
                    $"in the DI system.");
                continue;
            }

            // Call property update
            try
            {
                logger.LogInformation($"Updating | {propertyName}");
                await propertyModel.UpdateAsync(desiredProperties);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error | {propertyName}");
            }

        }
    }

}
