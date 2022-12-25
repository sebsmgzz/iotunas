namespace IoTunas.Extensions.Telemetry.Hosting.Emission;

using IoTunas.Extensions.Telemetry.Collections;
using IoTunas.Extensions.Telemetry.Factories;
using IoTunas.Extensions.Telemetry.Models;
using Microsoft.Extensions.DependencyInjection;

public class EmissionServiceBuilder : IEmissionServiceBuilder
{

    public EmissaryServiceCollection Emissaries { get; }

    public EmissionServiceBuilder()
    {
        Emissaries = new EmissaryServiceCollection();
    }

    public void Build(IServiceCollection services)
    {
        var mapping = new Dictionary<Type, EmissaryDescriptor>();
        foreach (var descriptor in Emissaries)
        {
            services.AddScoped(descriptor.Type);
            mapping.Add(descriptor.Type, descriptor);
        }
        services.AddHostedService<EmissionHost>();
        services.AddSingleton<IEmissaryControllerFactory>(p => new EmissaryControllerFactory(mapping, p));
        services.AddSingleton<IEmissaryFactory>(p => new EmissaryFactory(mapping, p));
        services.AddScoped<IMessageFactory, MessageFactory>();
    }

}
