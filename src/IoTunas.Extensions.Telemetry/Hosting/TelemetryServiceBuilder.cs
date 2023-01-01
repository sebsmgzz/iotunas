namespace IoTunas.Extensions.Telemetry.Hosting;

using IoTunas.Extensions.Telemetry.Collections;
using IoTunas.Extensions.Telemetry.Emission;
using IoTunas.Extensions.Telemetry.Models.Emission;
using IoTunas.Extensions.Telemetry.Reception;
using Microsoft.Extensions.DependencyInjection;

public class TelemetryServiceBuilder : ITelemetryServiceBuilder
{

    private readonly MetaProviderCollection providers;
    private readonly MetaReceiverCollection receivers;

    public IMetaProviderCollection Providers => providers;

    public IMetaReceiverCollection Receivers => receivers;

    public TelemetryServiceBuilder()
    {
        providers = new MetaProviderCollection();
        receivers = new MetaReceiverCollection();
    }

    public void AddEmissionServices(IServiceCollection services)
    {
        // For each telemetry provider:
        // 1. Add the provider's implementation type as scoped
        // 2. If a loop is defined in the provider:
        //   Get controller types and add them as a singleton
        // NOTE: Here we relay heavily in reflection to map the corresponding
        // controller's types from a given provider's type.
        // How to reduce reflection load here?
        // How badly is the application affected by using reflection this way?
        // How badly affected can it become?
        var controllers = new MetaControllerCollection();
        foreach (var provider in Providers)
        {
            services.AddScoped(provider.Type);
            if(MetaController.HasLoop(provider))
            {
                var controllerAbstraction = provider.GetControllerAbstraction();
                var controllerImplementation = provider.GetControllerImplementation();
                controllers.Add(controllerImplementation, provider);
                services.AddSingleton(controllerAbstraction, controllerImplementation);
            }
        }
        services.AddHostedService<EmissionService>();
        services.AddSingleton<IReadOnlyMetaProviderCollection>(providers);
        services.AddSingleton<IReadOnlyMetaControllerCollection>(controllers);
        services.AddScoped<ITelemetrySender, TelemetrySender>();
        services.AddScoped<IMessageFactory, MessageFactory>();
    }

    public void AddReceptionServices(IServiceCollection services)
    {
        foreach(var receiver in Receivers)
        {
            services.AddScoped(receiver.Type);
        }
        services.AddHostedService<ReceptionService>();
        services.AddSingleton<IReadOnlyMetaReceiverCollection>(receivers);
        services.AddSingleton<IReceiverMediator, ReceiverMediator>();
        services.AddSingleton<IReceiverFactory, ReceiverFactory>();
    }

}
