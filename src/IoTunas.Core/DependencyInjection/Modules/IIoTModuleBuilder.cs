namespace IoTunas.Core.DependencyInjection.Modules;

using IoTunas.Core.DependencyInjection;
using IoTunas.Core.Services.ClientBuilders.Modules;

public interface IIoTModuleBuilder : IIoTBuilder
{
    
    IModuleClientBuilder Client { get; }

}
