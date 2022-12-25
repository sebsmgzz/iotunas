namespace IoTunas.Extensions.Commands.Hosting;

using IoTunas.Core.DependencyInjection;
using IoTunas.Extensions.Commands.Collections;
using IoTunas.Extensions.Commands.Hosting.Commands;
using System;

public static class IoTBuilderExtensions
{

    public static void UseCommandHandlers(
        this IIoTBuilder builder,
        Action<ICommandsServiceBuilder>? configureAction = null)
    {
        builder.Services.AddCommandHandling(configureAction);
    }

}
