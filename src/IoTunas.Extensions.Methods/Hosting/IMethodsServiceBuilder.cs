namespace IoTunas.Extensions.Methods.Hosting;

using IoTunas.Extensions.Methods.Collections;

public interface IMethodsServiceBuilder
{

    IMetaCommandCollection Commands { get; }

}
