namespace IoTunas.Extensions.Methods.Services.Factories;

using IoTunas.Extensions.Methods.Models;

public interface ICommandFactory
{

    bool TryGet(string methodName, out ICommand handler);

}
