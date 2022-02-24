namespace IoTunas.Commands.Collections;

using IoTunas.Commands.Models;

public interface ICommandHandlerMapping
{

    int Count { get; }

    void AddHandler<T>(string methodName) where T : ICommandHandler;

    void AddHandler<T>() where T : ICommandHandler;

    void MapHandlers();

    IReadOnlyDictionary<string, Type> AsReadOnlyDictionary();

}
