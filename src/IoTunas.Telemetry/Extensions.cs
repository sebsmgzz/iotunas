namespace IoTunas.Telemetry;

using IoTunas.Core.Builders;
using IoTunas.Telemetry.Builders;
using IoTunas.Telemetry.Factories;
using IoTunas.Telemetry.Mediators;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class Extensions
{

    public static void UseTelemetryInputs(
        this IIoTBuilder iotBuilder,
        Action<IInputBrokersMappingBuilder> configureAction)
    {
        iotBuilder.ConfigureServices(services =>
        {

            // Add input telemetry brokers
            var builder = new InputBrokersMappingBuilder();
            configureAction.Invoke(builder);
            var mappingDict = builder.Build();
            foreach (var map in mappingDict)
            {
                services.AddScoped(map.Value);
            }

            // Add input telemetry broker services
            services.AddSingleton<IInputTelemetryMediator, InputTelemetryMediator>();
            services.AddSingleton<ITelemetryEndpointFactory, TelemetryEndpointFactory>(
                provider => new TelemetryEndpointFactory(provider, mappingDict));

        });
    }

    public static void UseTelemetryOutputs(this IIoTBuilder iotBuilder)
    {
        iotBuilder.ConfigureServices(services =>
        {
            services.AddSingleton<IOutputBatchMediatorFactory, OutputBatchMediatorFactory>();
            services.AddSingleton<IOutputTelemetryMediator, OutputTelemetryMediator>();
            services.AddSingleton<IMessageFactory, MessageFactory>();
        });
    }

}
