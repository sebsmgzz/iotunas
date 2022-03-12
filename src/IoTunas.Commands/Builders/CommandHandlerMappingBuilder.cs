namespace IoTunas.Extensions.Commands.Builders;

using IoTunas.Extensions.Commands.Models;
using IoTunas.Extensions.Commands.Reflection;
using System.Reflection;

public class CommandHandlerMappingBuilder : ICommandHandlerMappingBuilder
{

    private readonly Dictionary<string, Type> mappings = new();

    public int Count => mappings.Count;

    private void AddHandler(string methodName, Type handlerType)
    {
        if (!handlerType.IsAssignableTo(typeof(ICommandHandler)))
        {
            throw new InvalidOperationException(
                $"A handler must implement the {nameof(ICommandHandler)} " +
                $"interface in order to handle commands.");
        }
        mappings.Add(methodName, handlerType);
    }

    public void AddHandler<T>(string methodName) where T : ICommandHandler
    {
        mappings.Add(methodName, typeof(T));
    }

    private void AddHandler(Type handlerType)
    {
        var attribute = handlerType.GetCustomAttribute<CommandNameAttribute>();
        var methodName = attribute?.Value ?? handlerType.Name;
        AddHandler(methodName, handlerType);
    }

    public void AddHandler<T>() where T : ICommandHandler
    {
        AddHandler(typeof(T));
    }

    public void MapHandlers()
    {
        var interfaceType = typeof(ICommandHandler);
        var assembly = Assembly.GetEntryAssembly()!;
        var types = assembly.GetTypes();
        foreach (var handlerType in types)
        {
            if (handlerType.IsAssignableTo(interfaceType))
            {
                AddHandler(handlerType);
            }
        }
    }

    public IReadOnlyDictionary<string, Type> Build()
    {
        return new Dictionary<string, Type>(mappings);
    }

}
