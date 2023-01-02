namespace IoTunas.Extensions.Methods.Collections;

using IoTunas.Core.Collections;
using IoTunas.Extensions.Methods.Models.Commands;

public interface IMetaCommandCollection : 
    IMetaTypeCollection<MetaCommand>, 
    IReadOnlyMetaCommandCollection
{

    bool Add(Type type, string methodName);

    bool Add<TType>(string methodName) where TType : ICommand;


    bool Remove(string methodName);

}