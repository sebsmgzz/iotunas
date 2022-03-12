namespace IoTunas.Extensions.Telemetry;

using IoTunas.Core.Builders.Containers;
using IoTunas.Extensions.Telemetry.Builders;
using IoTunas.Extensions.Telemetry.Factories;
using IoTunas.Extensions.Telemetry.Mediators;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class Extensions
{

    public static void UseTelemetryOutputs(this IoTModuleBuilder module)
    {
        module.Services.AddSingleton<IOutputBatchMediatorFactory, OutputBatchMediatorFactory>();
        module.Services.AddSingleton<IOutputTelemetryMediator, OutputTelemetryMediator>();
        module.Services.AddSingleton<IMessageFactory, MessageFactory>();
    }

    public static void UseTelemetryInputs(
        this IoTDeviceBuilder device,
        Action<IInputBrokersMappingBuilder> configureAction)
    {
        device.Services.AddTelemetryInputAdHoc(configureAction);
        device.AfterBuild(async provider =>
        {
            var client = provider.GetRequiredService<DeviceClient>();
            var mediator = provider.GetRequiredService<IInputTelemetryMediator>();
            await client.SetReceiveMessageHandlerAsync(
                mediator.ReceiveAsync, null, default);
        });
    }

    public static void UseTelemetryInputs(
        this IoTModuleBuilder module,
        Action<IInputBrokersMappingBuilder> configureAction)
    {
        module.Services.AddTelemetryInputAdHoc(configureAction);
        module.AfterBuild(async provider =>
        {
            var client = provider.GetRequiredService<ModuleClient>();
            var mediator = provider.GetRequiredService<IInputTelemetryMediator>();
            await client.SetMessageHandlerAsync(
                mediator.ReceiveAsync, null, default);
        });
    }

    private static void AddTelemetryInputAdHoc(
        this IServiceCollection services,
        Action<IInputBrokersMappingBuilder> configureAction)
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

    }

}
