namespace IoTunas.Extensions.Commands.Hosting.Commands;

using IoTunas.Extensions.Commands.Collections;

public interface ICommandsServiceBuilder
{

    CommandServiceCollection Commands { get; }

}
