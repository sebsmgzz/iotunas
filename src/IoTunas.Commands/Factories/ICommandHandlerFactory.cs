namespace IoTunas.Commands.Factories;

using IoTunas.Commands.Models;

public interface ICommandHandlerFactory
{

    int Count { get; }

    bool Contains(string methodName);

    bool TryGet(string methodName, out ICommandHandler handler);

    IReadOnlyDictionary<string, Type> AsReadOnlyDictionary();

}
