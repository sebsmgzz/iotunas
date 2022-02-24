namespace IoTunas.Twin.Builders;

using IoTunas.Twin.Collections;
using IoTunas.Twin.Factories;
using IoTunas.Twin.Mediators;
using IoTunas.Twin.Models;
using Microsoft.Extensions.DependencyInjection;

public class TwinServicesBuilder : ITwinServicesBuilder
{

    private readonly IServiceCollection services;

    public TwinServicesBuilder(IServiceCollection services)
    {
        this.services = services;
    }

    public void AddDesiredProperties(
        Action<ITwinPropertyMapping<DesiredProperty>> configure)
    {
        var mapping = new TwinPropertyMapping<DesiredProperty>();
        configure?.Invoke(mapping);
        var mappingDict = mapping.AsReadOnlyDictionary();
        foreach (var map in mappingDict)
        {
            services.AddSingleton(map.Value);
        }
        services.AddSingleton<IDesiredPropertiesMediator, DesiredPropertiesMediator>();
        services.AddSingleton<IPropertyFactory<DesiredProperty>>(
            provider => new PropertyFactory<DesiredProperty>(provider, mappingDict));
    }

    public void AddReportedProperties(
    Action<ITwinPropertyMapping<ReportedProperty>> configure)
    {
        var mapping = new TwinPropertyMapping<ReportedProperty>();
        configure.Invoke(mapping);
        var mappingDict = mapping.AsReadOnlyDictionary();
        foreach (var map in mappingDict)
        {
            services.AddSingleton(map.Value);
        }
        services.AddSingleton<IReportedPropertiesMediator, ReportedPropertiesMediator>();
        services.AddSingleton<IPropertyFactory<ReportedProperty>>(
            provider => new PropertyFactory<ReportedProperty>(provider, mappingDict));
    }

}
