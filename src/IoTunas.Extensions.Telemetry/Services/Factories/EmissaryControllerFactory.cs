namespace IoTunas.Extensions.Telemetry.Factories;

using System;
using System.Diagnostics.CodeAnalysis;
using IoTunas.Extensions.Telemetry.Controllers;
using IoTunas.Extensions.Telemetry.Models;

public class EmissaryControllerFactory : IEmissaryControllerFactory
{

    private readonly IReadOnlyDictionary<Type, EmissaryDescriptor> mapping;
    private readonly IServiceProvider provider;

    private readonly Dictionary<Type, IEmissaryController> controllers;

    public EmissaryControllerFactory(
        IReadOnlyDictionary<Type, EmissaryDescriptor> mapping,
        IServiceProvider provider)
    {
        this.mapping = mapping;
        this.provider = provider;
        controllers = new Dictionary<Type, IEmissaryController>();
    }

    public bool TryGet(Type type, [MaybeNullWhen(false)] out IEmissaryController controller)
    {
        if (controllers.TryGetValue(type, out controller))
        {
            return true;
        }
        if(mapping.TryGetValue(type, out var descriptor))
        {
            controller = new EmissaryController(descriptor, provider);
            controllers.Add(type, controller);
        }
        return false;
    }

    public IEnumerable<IEmissaryController> GetAll()
    {
        foreach(var pair in mapping)
        {
            if(TryGet(pair.Key, out var controller))
            {
                yield return controller;
            }
        }
    }

}
