namespace IoTunas.Extensions.Commands.Collections;

using IoTunas.Extensions.Commands.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

public interface ICommandHandlerMapping
{

    int Count { get; }

    void AddHandler(string methodName, Type handlerType);

    void AddHandler(Type handlerType);
    
    void AddHandler<T>() where T : ICommandHandler;
    
    void AddHandler<T>(string methodName) where T : ICommandHandler;
    
    bool Contains(string methodName);

    void MapHandlers(Assembly assembly);

    void MapHandlers();
    
    bool TryGetValue(string methodName, [MaybeNullWhen(false)] out Type methodType);

}