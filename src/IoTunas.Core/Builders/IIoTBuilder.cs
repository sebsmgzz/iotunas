namespace IoTunas.Core.Builders;

using IoTunas.Commands.Builders;
using IoTunas.Connectivity.Builders;
using IoTunas.Core.Building;
using IoTunas.Telemetry.Builders;
using IoTunas.Twin.Builders;
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
    /// Enables connectivity services from the <see cref="Connectivity"/> extension.
    /// </summary>
    /// <param name="configure">A</param>
    void UseConnectivityServices(
        Action<IConnectionServicesBuilder> configure);

    /// <summary>
    /// Enables direct methods services from the <see cref="Commands"/> extension.
    /// </summary>
    /// <param name="configure">An action to configure the direct methods services.</param>
    void UseDirectMethodServices(
        Action<ICommandServicesBuilder> configure);

    /// <summary>
    /// Enables routed message telemetry services from the <see cref="Telemetry"/> extension.
    /// </summary>
    /// <param name="configure">An action to configure the telemetry services.</param>
    void UseTelemetryServices(
        Action<ITelemetryServicesBuilder> configure);

    /// <summary>
    /// Enables twin properties services from the <see cref="Twin"/> extension.
    /// </summary>
    /// <param name="configure">An action to configure the twin services.</param>
    void UseTwinServices(
        Action<ITwinServicesBuilder> configure);

}