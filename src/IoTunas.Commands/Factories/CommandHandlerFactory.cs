namespace IoTunas.Commands.Factories;

using IoTunas.Commands.Models;
using Microsoft.Extensions.DependencyInjection;

public class CommandHandlerFactory : ICommandHandlerFactory
{

    private readonly IServiceProvider serviceProvider;
    private readonly IReadOnlyDictionary<string, Type> mapping;

    public int Count => mapping.Count;

    public CommandHandlerFactory(
        IServiceProvider serviceProvider,
        IReadOnlyDictionary<string, Type> mapping)
    {
        this.serviceProvider = serviceProvider;
        this.mapping = mapping;
    }

    public bool Contains(string methodName)
    {
        return mapping.ContainsKey(methodName);
    }

    public bool TryGet(string methodName, out ICommandHandler handler)
    {
        if (!mapping.TryGetValue(methodName, out var methodType))
        {
            handler = null;
            return false;
        }
        var scope = serviceProvider.CreateScope();
        var service = scope.ServiceProvider.GetService(methodType);
        handler = service as ICommandHandler;
        return handler != null;
    }

    public IReadOnlyDictionary<string, Type> AsReadOnlyDictionary()
    {
        return mapping;
    }

}
