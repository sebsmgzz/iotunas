namespace IoTunas.Core.Builders;

using IoTunas.Commands.Builders;
using IoTunas.Connectivity.Builders;
using IoTunas.Core.Building;
using IoTunas.Core.Services;
using IoTunas.Telemetry.Builders;
using IoTunas.Twin.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

/// <inheritdoc cref="IIoTBuilder"/>
internal class IoTBuilder : IIoTBuilder
{

    private Action<IClientBuilder>? configureClientAction;
    private readonly List<Action<IServiceCollection>> configureActions;

    public IConfiguration Configuration { get; }

    public IHostEnvironment Environment { get; }

    public IoTBuilder(
        HostBuilderContext context)
        : this(context.Configuration, context.HostingEnvironment)
    {
    }

    public IoTBuilder(
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
        configureClientAction = null;
        configureActions = new List<Action<IServiceCollection>>();
    }

    public void ConfigureClient(Action<IClientBuilder> configureClientAction)
    {
        this.configureClientAction = configureClientAction;
    }

    public void UseDirectMethodServices(
        Action<ICommandServicesBuilder> configure)
    {
        configureActions.Add(
            services => configure.Invoke(
                new CommandServicesBuilder(services)));
    }

    public void UseTelemetryServices(
        Action<ITelemetryServicesBuilder> configure)
    {
        configureActions.Add(
            services => configure.Invoke(
                new TelemetryServicesBuilder(services)));
    }

    public void UseConnectivityServices(
        Action<IConnectionServicesBuilder> configure)
    {
        configureActions.Add(
            services => configure.Invoke(
                new ConnectionServicesBuilder(services)));
    }

    public void UseTwinServices(
        Action<ITwinServicesBuilder> configure)
    {
        configureActions.Add(
            services => configure.Invoke(
                new TwinServicesBuilder(services)));
    }

    /// <summary>
    /// Builds the configured services into the provided service collection
    /// </summary>
    /// <param name="services">The service collection where to map the services.</param>
    public void Build(IServiceCollection services)
    {
        services.AddHostedService(provider => provider.GetRequiredService<HostService>());
        services.AddSingleton(provider =>
        {
            var builder = ClientBuilder.FromEnvironment();
            configureClientAction?.Invoke(builder);
            return new HostService(builder);
        });
        services.AddSingleton(provider =>
        {
            var clientHost = provider.GetRequiredService<HostService>();
            return clientHost.Client;
        });
        configureActions.ForEach(configureAction =>
            configureAction.Invoke(services));
    }

}
