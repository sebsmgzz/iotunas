namespace IoTunas.Core;

using IoTunas.Core.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

/// <summary>
/// Provides extension methods to initialize an IHostBuilder
/// with IoT capabilities.
/// </summary>
public static class Extensions
{

    /// <summary>
    /// Configures the host builder with the default iot edge capabilities.
    /// Adds IOTEDGE_ environment variables.
    /// </summary>
    /// <param name="hostBuilder">The host builder to configure.</param>
    /// <param name="configureBuilder">An action to configure the iot edge builder.</param>
    /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
    public static IHostBuilder ConfigureIoTEdgeDefaults(
        this IHostBuilder hostBuilder,
        Action<IIoTBuilder> configureBuilder)
    {
        return hostBuilder
            .ConfigureAppConfiguration(config =>
                config.AddEnvironmentVariables("IOTEDGE_"))
            .ConfigureServices((context, services) =>
            {
                var builder = new IoTBuilder(context);
                configureBuilder.Invoke(builder);
                builder.Build(services);
            });
    }

}
