namespace IoTunas.Core.Builders;

using IoTunas.Core.Building;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

/// <summary>
/// Represents the configuration available to enable
/// the core services for an iot edge module.
/// </summary>
public interface IIoTBuilder
{

    /// <summary>
    /// The configuration properties.
    /// </summary>
    IConfiguration Configuration { get; }

    /// <summary>
    /// The environment properties.
    /// </summary>
    IHostEnvironment Environment { get; }

    /// <summary>
    /// Applies current iot configuration to the provided services.
    /// </summary>
    /// <param name="services">The services to which to apply the configuration.</param>
    void Build(IServiceCollection services);

    /// <summary>
    /// Configures the module's client initialization.
    /// </summary>
    /// <param name="configureClientAction">An action to configure the client builder.</param>
    void ConfigureClient(
        Action<IClientBuilder> configureClientAction);
    /// <summary>
    /// Configures the services available in the DI.
    /// Calls to this method are acumulated.
    /// </summary>
    /// <param name="configureAction">The method to invoke whenever the configuration of the services takes place.</param>
    void ConfigureServices(Action<IServiceCollection> configureAction);

}