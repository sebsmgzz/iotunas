namespace IoTunas.Extensions.Telemetry.Hosting;

using IoTunas.Core.DependencyInjection.Devices;
using IoTunas.Core.DependencyInjection.Modules;
using IoTunas.Extensions.Telemetry.Collections;
using IoTunas.Extensions.Telemetry.Factories;
using IoTunas.Extensions.Telemetry.Mediators;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class InputExtensions
{

    public static IoTDeviceBuilder UseTelemetryInputs(
        this IoTDeviceBuilder device,
        Action<IInputBrokerDefinitionMapping>? configureAction)
    {
        device.Services.AddHostedService<DeviceInputTelemetryService>();
        device.Services.AddTelemetryInputAdHoc(configureAction);
        return device;
    }

    public static IoTModuleBuilder UseTelemetryInputs(
        this IoTModuleBuilder module,
        Action<IInputBrokerDefinitionMapping>? configureAction)
    {
        module.Services.AddHostedService<ModuleInputTelemetryService>();
        module.Services.AddTelemetryInputAdHoc(configureAction);
        return module;
    }

    private static void AddTelemetryInputAdHoc(
        this IServiceCollection services,
        Action<IInputBrokerDefinitionMapping>? configureAction)
    {
        var mapping = new InputBrokerDefinitionMapping();
        configureAction?.Invoke(mapping);
        services.AddSingleton<IInputBrokerDefinitionMapping>(mapping);
        foreach (var item in mapping)
        {
            services.AddScoped(item.Value.BrokerType);
        }
        services.AddSingleton<IInputBrokerFactory, InputBrokerFactory>();
        services.AddSingleton<IInputBrokerMediator, InputBrokerMediator>();
    }

}
