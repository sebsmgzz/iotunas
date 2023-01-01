namespace IoTunas.Extensions.Telemetry.Emission;

using IoTunas.Core.DependencyInjection;
using IoTunas.Core.Services.ClientHosts;
using IoTunas.Extensions.Telemetry.Collections;
using IoTunas.Extensions.Telemetry.Models.Emission;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

public class TelemetrySender : ITelemetrySender
{

    public const string InvalidOutputNameLog = "Invalid Output Name | " +
        "The output name '{outputName}' is not valid for an edge capable client.";
    public const string ProviderNotFoundLog = "Provider Not Found | " +
        "Provider of type {providerType} was not found in the service provider DI.";
    public const string MetaProviderNotFoundLog = "MetaProvider Not Found | " +
        "Meta provider for type {providerType} was not found in the meta provider collection.";
    public const string InvalidCastingLog = "Invalid Casting | " +
        "Failed to cast {interfaceType} into {providerType}." +
        "Ignoring additional action.";
    public const string ProviderErrorLog = "Telemetry Provider Error | " +
        "There was an unexcepted exception while calling the provider {providerType}.";

    private readonly IReadOnlyMetaProviderCollection metaProviders;
    private readonly IServiceProvider serviceProvider;
    private readonly IIoTClientHost clientHost;
    private readonly ILogger logger;

    public TelemetrySender(
        IReadOnlyMetaProviderCollection metaProviders,
        IServiceProvider serviceProvider,
        IIoTClientHost clientHost,
        ILogger<ITelemetrySender> logger)
    {
        this.metaProviders = metaProviders;
        this.serviceProvider = serviceProvider;
        this.clientHost = clientHost;
        this.logger = logger;
    }

    public async Task<bool> SendBatchAsync<TTelemetry>(
        IEnumerable<TTelemetry> telemetry,
        string? outputName = null,
        CancellationToken cancellationToken = default)
        where TTelemetry : ITelemetry
    {
        var messages = telemetry.Select(telemetry => telemetry.AsMessage());
        if (!clientHost.IsEdgeCapable)
        {
            var client = serviceProvider.GetRequiredService<DeviceClient>();
            await client.SendEventBatchAsync(messages, cancellationToken);
            return true;
        }
        else if (outputName != null)
        {
            var client = serviceProvider.GetRequiredService<ModuleClient>();
            await client.SendEventBatchAsync(outputName, messages, cancellationToken);
            return true;
        }
        else
        {
            logger.LogError(InvalidOutputNameLog, outputName);
            return false;
        }
    }

    public async Task<bool> SendAsync<TTelemetry>(
        TTelemetry telemetry,
        string? outputName = null,
        CancellationToken cancellationToken = default)
        where TTelemetry : ITelemetry
    {
        var message = telemetry.AsMessage();
        if (!clientHost.IsEdgeCapable)
        {
            var client = serviceProvider.GetRequiredService<DeviceClient>();
            await client.SendEventAsync(message, cancellationToken);
            return true;
        }
        else if (outputName != null)
        {
            var client = serviceProvider.GetRequiredService<ModuleClient>();
            await client.SendEventAsync(outputName, message, cancellationToken);
            return true;
        }
        else
        {
            logger.LogError(InvalidOutputNameLog, outputName);
            return false;
        }
    }


    public async Task<bool> SendAsync(
        Type providerType,
        Action<ITelemetryProvider<ITelemetry>>? action = null,
        CancellationToken cancellationToken = default)
    {

        // Get provider service
        if (!serviceProvider.TryGetCastedService<ITelemetryProvider<ITelemetry>>(
            serviceType: providerType,
            service: out var telemetryProvider))
        {
            logger.LogCritical(ProviderNotFoundLog, providerType);
            return false;
        }

        // Get meta provider
        if (!metaProviders.TryGet(providerType, out var metaProvider))
        {
            logger.LogCritical(MetaProviderNotFoundLog, providerType);
            return false;
        }

        // Send telemetry
        try
        {
            action?.Invoke(telemetryProvider);
            var telemetry = telemetryProvider.GetTelemetry();
            return await SendAsync(telemetry, metaProvider.Output.Name, cancellationToken);
        }
        catch(Exception ex)
        {
            logger.LogError(ex, ProviderErrorLog, providerType);
            return false;
        }

    }

    public Task<bool> SendAsync<TProvider>(
        Action<TProvider>? action = null,
        CancellationToken cancellationToken = default)
        where TProvider : class, ITelemetryProvider<ITelemetry>
    {
        var providerType = typeof(TProvider);
        return SendAsync(providerType, telemetryProvider =>
        {
            if (telemetryProvider is TProvider castedProvider)
            {
                action?.Invoke(castedProvider!);
            }
            else
            {
                var interfaceType = typeof(ITelemetryProvider<ITelemetry>);
                logger.LogWarning(InvalidCastingLog, interfaceType, providerType);
            }
        }, cancellationToken);
    }

}
