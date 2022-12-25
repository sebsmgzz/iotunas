namespace IoTunas.Core.DependencyInjection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public abstract class IoTBuilderBase : IIoTBuilder
{

    private readonly HostBuilderContext context;

    public IConfiguration Configuration => context.Configuration;

    public IHostEnvironment Environment => context.HostingEnvironment;

    public IServiceCollection Services { get; }

    public IoTBuilderBase(
        HostBuilderContext context,
        IServiceCollection services)
    {
        this.context = context;
        Services = services;
    }

    public abstract IServiceProvider BuildServiceProvider();

}
