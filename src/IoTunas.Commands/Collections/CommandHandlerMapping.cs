namespace IoTunas.Extensions.Commands.Collections;

using IoTunas.Core.Collections;
using IoTunas.Extensions.Commands.Models;
using IoTunas.Extensions.Commands.Reflection;
using System;
using System.Reflection;

public sealed class CommandHandlerMapping : SimpleStringMapping<ICommandHandler>, ICommandHandlerMapping
{

    protected override string CreateKey(Type implementationType)
    {
        var attribute = implementationType.GetCustomAttribute<CommandNameAttribute>();
        return attribute?.Value ?? base.CreateKey(implementationType);
    }

}
