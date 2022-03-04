namespace IoTunas.Twin;

using IoTunas.Core.Builders;
using IoTunas.Twin.Builders;
using IoTunas.Twin.Factories;
using IoTunas.Twin.Mediators;
using IoTunas.Twin.Models;
using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{

    public static void UseTwinDesiredProperties(
        this IIoTBuilder iotBuilder,
        Action<ITwinPropertyMappingBuilder<DesiredProperty>> configureAction)
    {
        iotBuilder.ConfigureServices(services =>
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

        });
    }

    public static void UseTwinReportedProperties(
        this IIoTBuilder iotBuilder,
        Action<ITwinPropertyMappingBuilder<ReportedProperty>> configureAction)
    {
        iotBuilder.ConfigureServices(services =>
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
        
        });
    }

}
