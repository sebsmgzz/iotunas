namespace IoTunas.Extensions.Commands.Factories;

using IoTunas.Extensions.Commands.Models;

public interface ICommandHandlerFactory
{

    bool Contains(string methodName);

    bool TryGet(string methodName, out ICommandHandler handler);

}
