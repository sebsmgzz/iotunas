namespace IoTunas.Commands;

using IoTunas.Commands.Builders;
using IoTunas.Commands.Factories;
using IoTunas.Commands.Mediators;
using IoTunas.Core.Builders;
using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{

    public static void AddCommandInvokers(this IIoTBuilder iotBuilder)
    {
        iotBuilder.ConfigureServices(services =>
        {
            services.AddSingleton<ICommandInvokerMediator, CommandInvokerMediator>();
        });
    }

    public static void AddCommandHandlers(
        this IIoTBuilder iotBuilder,
        Action<ICommandHandlerMappingBuilder> configureAction)
    {
        iotBuilder.ConfigureServices(services =>
        {

            // Add handlers
            var builder = new CommandHandlerMappingBuilder();
            configureAction.Invoke(builder);
            var mapping = builder.Builder();
            foreach (var pair in mapping)
            {
                services.AddScoped(pair.Value);
            }

            // Add command services
            services.AddSingleton<ICommandHandlerMediator, CommandHandlerMediator>();
            services.AddSingleton<IMethodResponseFactory, MethodResponseFactory>();
            services.AddSingleton<ICommandHandlerFactory, CommandHandlerFactory>(
                provider => new CommandHandlerFactory(provider, mapping));

        });
    }

}
