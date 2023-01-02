namespace IoTunas.Extensions.Methods.Collections;

using IoTunas.Core.Collections;
using IoTunas.Extensions.Methods.Models.Commands;
using System;

public interface IReadOnlyMetaCommandCollection :
    IReadOnlyMetaTypeCollection<MetaCommand>,
    IEnumerable<MetaCommand>
{
    
    IReadOnlyDictionary<string, Type> AsMapping();

}
