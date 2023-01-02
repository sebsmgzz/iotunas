namespace IoTunas.Extensions.Methods.Hosting;

using IoTunas.Core.DependencyInjection;
using System;

public static class IoTBuilderExtensions
{

    public static void UseMethodServices(
        this IIoTBuilder iotBuilder,
        Action<IMethodsServiceBuilder>? configureAction = null)
    {
        iotBuilder.Services.AddMethodServices(configureAction);
    }

    public static void MapCommandServices(this IIoTBuilder iotBuilder)
    {
        iotBuilder.UseMethodServices(builder =>
        {
            builder.Commands.Map();
        });
    }

    public static void MapCommands(this IIoTBuilder iotBuilder)
    {
        iotBuilder.Services.AddCommandsOnly(builder => builder.Commands.Map());
    }

}
