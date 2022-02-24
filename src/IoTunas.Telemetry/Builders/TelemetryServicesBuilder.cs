namespace IoTunas.Telemetry.Builders;

using IoTunas.Telemetry.Factories;
using IoTunas.Telemetry.Mediators;
using IoTunas.Telemetry.Receivers.Collections;
using Microsoft.Extensions.DependencyInjection;

public class TelemetryServicesBuilder : ITelemetryServicesBuilder
{

    private readonly IServiceCollection services;

    public TelemetryServicesBuilder(IServiceCollection services)
    {
        this.services = services;
    }

    public void AddInputBrokers(Action<IInputTelemetryBrokerMapping> configure)
    {
        var mapping = new InputTelemetryBrokerMapping();
        configure?.Invoke(mapping);
        var mappingDict = mapping.AsReadOnlyDictionary();
        foreach(var map in mappingDict)
        {
            services.AddScoped(map.Value);
        }
        services.AddSingleton<IInputTelemetryMediator, InputTelemetryMediator>();
        services.AddSingleton<ITelemetryEndpointFactory, TelemetryEndpointFactory>(
            provider => new TelemetryEndpointFactory(
                provider, mapping.AsReadOnlyDictionary()));

    }

    public void AddOutputBrokers()
    {
        services.AddSingleton<IOutputBatchMediatorFactory, OutputBatchMediatorFactory>();
        services.AddSingleton<IOutputTelemetryMediator, OutputTelemetryMediator>();
        services.AddSingleton<IMessageFactory, MessageFactory>();
    }

}
