namespace IoTunas.Commands.Collections;

using IoTunas.Commands.Models;
using IoTunas.Commands.Reflection;
using System.Reflection;

public class CommandHandlerMapping : ICommandHandlerMapping
{

    private readonly Dictionary<string, Type> mapping = new();

    public int Count => mapping.Count;

    public void AddHandler<T>(string methodName) where T : ICommandHandler
    {
        mapping.Add(methodName, typeof(T));
    }

    public void AddHandler<T>() where T : ICommandHandler
    {
        AddHandler(typeof(T));
    }

    // Keep it private to ensure type implements interface
    private void AddHandler(Type handlerType)
    {
        var attribute = handlerType.GetCustomAttribute<CommandNameAttribute>();
        var methodName = attribute?.Value ?? handlerType.Name;
        mapping.Add(methodName, handlerType);
    }

    public void MapHandlers()
    {
        var assembly = Assembly.GetEntryAssembly();
        var types = assembly.GetTypes();
        var handlerType = typeof(ICommandHandler);
        foreach (var type in types)
        {
            if (handlerType.IsAssignableFrom(type))
            {
                AddHandler(handlerType);
            }
        }
    }

    public IReadOnlyDictionary<string, Type> AsReadOnlyDictionary()
    {
        return mapping;
    }

}
