namespace IoTunas.Core.DependencyInjection;

using IoTunas.Core.DependencyInjection.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public delegate IIoTContainerBuilder IoTContainerBuilderFactory(
    HostBuilderContext context,
    IServiceCollection services);
