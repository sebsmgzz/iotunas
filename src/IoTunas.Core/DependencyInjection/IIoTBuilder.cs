namespace IoTunas.Core.DependencyInjection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public interface IIoTBuilder
{

    public IConfiguration Configuration { get; }

    public IHostEnvironment Environment { get; }

    public IServiceCollection Services { get; }

    IServiceProvider BuildServiceProvider();

}
