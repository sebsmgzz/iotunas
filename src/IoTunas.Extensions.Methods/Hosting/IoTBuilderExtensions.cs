namespace IoTunas.Extensions.Methods.Hosting;

using IoTunas.Core.DependencyInjection;
using IoTunas.Extensions.Methods.Hosting.Commands;
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
