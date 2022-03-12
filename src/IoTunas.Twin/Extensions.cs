namespace IoTunas.Extensions.Twin;

using IoTunas.Core.Builders.Containers;
using IoTunas.Extensions.Twin.Builders;
using IoTunas.Extensions.Twin.Factories;
using IoTunas.Extensions.Twin.Mediators;
using IoTunas.Extensions.Twin.Models;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{

    public static void UseTwinDesiredProperties(
        this IoTModuleBuilder module,
        Action<ITwinPropertyMappingBuilder<DesiredProperty>> configureAction)
    {
        module.Services.AddTwinDesiredPropertiesServicesAdHoc(configureAction);
        module.AfterBuild(async provider =>
        {
            var client = provider.GetRequiredService<ModuleClient>();
            var mediator = provider.GetRequiredService<IDesiredPropertiesMediator>();
            await client.SetDesiredPropertyUpdateCallbackAsync(
                mediator.UpdateDesiredPropertiesAsync, null, default);
        });
    }

    public static void UseTwinDesiredProperties(
        this IoTModuleBuilder module,
        Action<ITwinPropertyMappingBuilder<ReportedProperty>> configureAction)
    {
        module.Services.AddTwinReportedPropertiesServicesAdHoc(configureAction);
    }

    private static void AddTwinDesiredPropertiesServicesAdHoc(
        this IServiceCollection services,
        Action<ITwinPropertyMappingBuilder<DesiredProperty>> configureAction)
    {

        /// Add desired properties models
        var builder = new TwinPropertyMappingBuilder<DesiredProperty>();
        configureAction.Invoke(builder);
        var mapping = builder.Build();
        foreach (var map in mapping)
        {
            services.AddSingleton(map.Value);
        }

        // Add required services for twin desired properties
        services.AddSingleton<IDesiredPropertiesMediator, DesiredPropertiesMediator>();
        services.AddSingleton<IPropertyFactory<DesiredProperty>>(
            provider => new PropertyFactory<DesiredProperty>(provider, mapping));

    }

    private static void AddTwinReportedPropertiesServicesAdHoc(
        this IServiceCollection services,
        Action<ITwinPropertyMappingBuilder<ReportedProperty>> configureAction)
    {

        // Add reported properties models
        var builder = new TwinPropertyMappingBuilder<ReportedProperty>();
        configureAction.Invoke(builder);
        var mapping = builder.Build();
        foreach (var map in mapping)
        {
            services.AddSingleton(map.Value);
        }

        // Add required services for twin desired properties
        services.AddSingleton<IReportedPropertiesMediator, ReportedPropertiesMediator>();
        services.AddSingleton<IPropertyFactory<ReportedProperty>>(
            provider => new PropertyFactory<ReportedProperty>(provider, mapping));

    }

}
