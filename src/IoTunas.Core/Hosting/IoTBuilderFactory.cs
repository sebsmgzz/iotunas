namespace IoTunas.Core.Hosting;

using IoTunas.Core.Builders.Containers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public delegate IIoTContainerBuilder IoTBuilderFactory(
    HostBuilderContext context,
    IServiceCollection services);
