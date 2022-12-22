namespace IoTunas.Extensions.Connectivity.Factories;

using IoTunas.Extensions.Connectivity.Builders;
using IoTunas.Extensions.Connectivity.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public class ConnectionObserverFactory : IConnectionObserverFactory
{

    public const string InvalidObserverLog =
        "Connection observer {oberserTypeName} needs to " +
        "inherit {interfaceName} and be registered " +
        "in the service provider's DI to be invoked.";

    private readonly IConnectionObserverMapping mapping;
    private readonly IServiceProvider provider;
    private readonly ILogger logger;
    
    public ConnectionObserverFactory(
        IServiceProvider provider,
        IConnectionObserverMapping mapping,
        ILogger<IConnectionObserverFactory> logger)
    {
        this.provider = provider;
        this.mapping = mapping;
        this.logger = logger;
    }

    public IEnumerable<IConnectionObserver> GetAll()
    {
        for(int i = 0; i < mapping.Count; i++)
        {
            if(TryGet(i, out var observer))
            {
                yield return observer;
            }
        }
    }

    public bool TryGet(int index, [MaybeNullWhen(false)] out IConnectionObserver observer)
    {
        var observerType = mapping[index];
        var scope = provider.CreateScope();
        var service = scope.ServiceProvider.GetService(observerType);
        observer = service as IConnectionObserver;
        if(observer == null)
        {
            logger.LogCritical(InvalidObserverLog, observerType.Name, nameof(IConnectionObserver));
        }
        return observer != null;
    }

}
