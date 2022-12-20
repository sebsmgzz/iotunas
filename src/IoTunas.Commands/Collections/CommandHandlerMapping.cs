namespace IoTunas.Extensions.Commands.Collections;

using IoTunas.Extensions.Commands.Models;
using IoTunas.Extensions.Commands.Reflection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

public class CommandHandlerMapping : ICommandHandlerMapping
{

    private readonly Dictionary<string, Type> mapping;

    public int Count => mapping.Count;

    public CommandHandlerMapping()
    {
        mapping = new Dictionary<string, Type>();
    }

    public void AddHandler(string methodName, Type handlerType)
    {
        if (!handlerType.IsAssignableTo(typeof(ICommandHandler)))
        {
            throw new InvalidOperationException(
                $"A handler must implement the {nameof(ICommandHandler)} " +
                $"interface in order to handle commands.");
        }
        mapping.Add(methodName, handlerType);
    }

    public void AddHandler<T>(string methodName) where T : ICommandHandler
    {
        mapping.Add(methodName, typeof(T));
    }

    public void AddHandler(Type handlerType)
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
        var assembly = Assembly.GetEntryAssembly();
        MapHandlers(assembly!);
    }

    public void MapHandlers(Assembly assembly)
    {
        var interfaceType = typeof(ICommandHandler);
        var types = assembly.GetTypes();
        foreach (var handlerType in types)
        {
            if (handlerType.IsAssignableTo(interfaceType))
            {
                AddHandler(handlerType);
            }
        }
    }

    public bool Contains(string methodName)
    {
        return mapping.ContainsKey(methodName);
    }

    public bool TryGetValue(string methodName, [MaybeNullWhen(false)] out Type methodType)
    {
        return mapping.TryGetValue(methodName, out methodType);
    }

}
