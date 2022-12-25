namespace IoTunas.Extensions.Telemetry.Hosting.Reception;

using IoTunas.Extensions.Telemetry.Collections;
using IoTunas.Extensions.Telemetry.Factories;
using IoTunas.Extensions.Telemetry.Mediators;
using Microsoft.Extensions.DependencyInjection;
using System;

public class ReceptionServiceBuilder : IReceptionServiceBuilder
{

    public ReceiverServiceCollection Receivers { get; }

    public ReceptionServiceBuilder()
    {
        Receivers = new ReceiverServiceCollection();
    }

    public void Build(IServiceCollection services)
    {
        var mapping = new Dictionary<string, Type>();
        foreach (var descriptor in Receivers)
        {
            services.AddScoped(descriptor.Type);
            mapping.Add(descriptor.InputName, descriptor.Type);
        }
        services.AddHostedService<ReceptionHost>();
        services.AddSingleton<IReceiverMediator, ReceiverMediator>();
        services.AddSingleton<IReceiverFactory>(p => new ReceiverFactory(mapping, p));
        services.AddScoped<IMessageFactory, MessageFactory>();
    }

}
