namespace IoTunas.Core.Builders.Containers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public abstract class IoTContainerBuilderBase : IIoTContainerBuilder
{

    public IConfiguration Configuration { get; }

    public IHostEnvironment Environment { get; }

    public IServiceCollection Services { get; }

    public IoTContainerBuilderBase(
        HostBuilderContext context,
        IServiceCollection services)
        : this(
              context.Configuration,
              context.HostingEnvironment,
              services)
    {
    }

    public IoTContainerBuilderBase(
        IConfiguration configuration,
        IHostEnvironment environment,
        IServiceCollection services)
    {
        Configuration = configuration;
        Environment = environment;
        Services = services;
    }

    public abstract IServiceProvider BuildServiceProvider();

}
