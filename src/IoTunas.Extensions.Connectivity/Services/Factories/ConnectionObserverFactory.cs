namespace IoTunas.Extensions.Connectivity.Factories;

using IoTunas.Core.Hosting;
using IoTunas.Extensions.Connectivity.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public class ConnectionObserverFactory : IConnectionObserverFactory
{

    public const string InvalidObserverLog =
        "Connection observer {observerTypeName} needs to " +
        "inherit {interfaceName} and be registered " +
        "in the service provider's DI to be invoked.";

    private readonly IReadOnlyList<Type> listing;
    private readonly IServiceProvider provider;
    private readonly ILogger logger;
    
    public ConnectionObserverFactory(
        IReadOnlyList<Type> listing,
        IServiceProvider provider)
    {
        this.provider = provider;
        this.listing = listing;
        logger = provider.GetRequiredService<ILogger<IConnectionObserverFactory>>();
    }

    public IEnumerable<IConnectionObserver> GetAll()
    {
        for(int i = 0; i < listing.Count; i++)
        {
            if(TryGet(i, out var observer))
            {
                yield return observer;
            }
        }
    }

    public bool TryGet(int index, [MaybeNullWhen(false)] out IConnectionObserver observer)
    {
        if(index < 0 || listing.Count <= index)
        {
            observer = null;
            return false;
        }
        var type = listing[index];
        if(!provider.TryGetService<IConnectionObserver>(type, out observer))
        {
            logger.LogCritical(InvalidObserverLog, type.Name, nameof(IConnectionObserver));
            observer = null;
            return false;
        }
        return observer != null;
    }

}
