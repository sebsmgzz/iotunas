namespace IoTunas.Extensions.Commands.Collections;

using IoTunas.Core.Collections;
using IoTunas.Extensions.Commands.Models;
using System;

public interface ICommandHandlerMapping : IMapping<string, Type, ICommandHandler>
{
}
