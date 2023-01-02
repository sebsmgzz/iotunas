namespace IoTunas.Extensions.Methods.Commands;

using IoTunas.Extensions.Methods.Models.Commands;

public interface ICommandFactory
{

    bool TryGet(string methodName, out ICommand handler);

}
