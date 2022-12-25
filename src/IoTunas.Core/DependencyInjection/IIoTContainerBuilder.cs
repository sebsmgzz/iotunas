namespace IoTunas.Core.DependencyInjection;

using System;

public interface IIoTContainerBuilder
{

    IServiceProvider BuildServiceProvider();

}
