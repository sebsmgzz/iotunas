namespace IoTunas.Extensions.Commands.Hosting.Commands;

using IoTunas.Extensions.Commands.Collections;

public interface ICommandServiceBuilder
{

    CommandServiceCollection Commands { get; }

}
