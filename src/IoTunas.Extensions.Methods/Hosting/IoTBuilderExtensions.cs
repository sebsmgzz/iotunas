namespace IoTunas.Extensions.Methods.Hosting;

using IoTunas.Core.DependencyInjection;
using IoTunas.Extensions.Methods.Hosting.Commands;
using System;

public static class IoTBuilderExtensions
{
    public static void UseCommands(this IIoTBuilder builder)
    {
        builder.Services.AddCommands();
    }

    public static void UseCommands(
        this IIoTBuilder builder,
        Action<ICommandsServiceBuilder> configureAction)
    {
        builder.Services.AddCommands(configureAction);
    }

}
