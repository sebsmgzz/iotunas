namespace IoTunas.Extensions.Telemetry.Factories;

using System;
using IoTunas.Extensions.Telemetry.Collections;
using System.Diagnostics.CodeAnalysis;
using IoTunas.Core.Hosting;
using IoTunas.Extensions.Telemetry.Controllers;

public class OutputBrokerControllerFactory : IOutputBrokerControllerFactory
{

    private readonly IServiceProvider provider;
    private readonly IOutputBrokerDefinitionMapping mapping;
    private readonly Dictionary<Type, IOutputBrokerController> controllers;

    public OutputBrokerControllerFactory(
        IServiceProvider provider,
        IOutputBrokerDefinitionMapping mapping)
    {
        this.provider = provider;
        this.mapping = mapping;
        controllers = new Dictionary<Type, IOutputBrokerController>();
    }

    public bool TryGet(Type type, [MaybeNullWhen(false)] out IOutputBrokerController controller)
    {
        if (controllers.TryGetValue(type, out controller))
        {
            return true;
        }
        foreach (var pair in mapping)
        {
            if (pair.Value.BrokerType.Equals(type) &&
                provider.TryGetService(type, out controller))
            {
                controller.Period = pair.Value.DefaultPeriod;
                controller.BrokerType = pair.Value.BrokerType;
                return true;
            }
        }
        return false;
    }

}
