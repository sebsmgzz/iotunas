namespace IoTunas.Extensions.Telemetry.Factories;

using IoTunas.Extensions.Telemetry.Models;
using Microsoft.Extensions.DependencyInjection;

public class TelemetryEndpointFactory : ITelemetryEndpointFactory
{

    private readonly IServiceProvider serviceProvider;
    private readonly IReadOnlyDictionary<string, Type> mapping;

    public int Count => mapping.Count;

    public TelemetryEndpointFactory(
        IServiceProvider serviceProvider,
        IReadOnlyDictionary<string, Type> mapping)
    {
        this.serviceProvider = serviceProvider;
        this.mapping = mapping;
    }

    public bool Contains(string endpointName)
    {
        return mapping.ContainsKey(endpointName);
    }

    public bool TryGet(string endpointName, out IInputTelemetryBroker receiver)
    {
        if (!mapping.TryGetValue(endpointName, out var endpointType))
        {
            receiver = null;
            return false;
        }
        var scope = serviceProvider.CreateScope();
        var service = scope.ServiceProvider.GetService(endpointType);
        receiver = service as IInputTelemetryBroker;
        return receiver != null;
    }

}
