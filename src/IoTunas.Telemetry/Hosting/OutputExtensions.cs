namespace IoTunas.Extensions.Telemetry.Hosting;

using IoTunas.Core.DependencyInjection.Builders;
using IoTunas.Extensions.Telemetry.Collections;
using IoTunas.Extensions.Telemetry.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class OutputExtensions
{
    public static IoTDeviceBuilder UseTelemetryOutputs(
        this IoTDeviceBuilder device,
        Action<IOutputBrokerDefinitionMapping>? configureAction)
    {
        device.Services.AddHostedService<DeviceOutputTelemetryService>();
        device.Services.AddTelemetryOutputAdHoc(configureAction);
        return device;
    }

    public static IoTModuleBuilder UseTelemetryOutputs(
        this IoTModuleBuilder module,
        Action<IOutputBrokerDefinitionMapping>? configureAction)
    {
        module.Services.AddHostedService<ModuleOutputTelemetryService>();
        module.Services.AddTelemetryOutputAdHoc(configureAction);
        return module;
    }

    private static IServiceCollection AddTelemetryOutputAdHoc(
        this IServiceCollection services,
        Action<IOutputBrokerDefinitionMapping>? configureAction)
    {
        var mapping = new OutputBrokerDefinitionMapping();
        configureAction?.Invoke(mapping);
        services.AddSingleton<IOutputBrokerDefinitionMapping>(mapping);
        foreach (var item in mapping)
        {
            services.AddScoped(item.Value.BrokerType);
        }
        services.AddScoped<IMessageFactory, MessageFactory>();
        // TODO
        return services;
    }

}
