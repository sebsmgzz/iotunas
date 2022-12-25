namespace IoTunas.Extensions.Methods.Hosting.Commands;

using IoTunas.Extensions.Methods.Collections;

public interface ICommandsServiceBuilder
{

    CommandServiceCollection Commands { get; }

}
