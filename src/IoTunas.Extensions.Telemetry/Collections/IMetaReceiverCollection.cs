namespace IoTunas.Extensions.Telemetry.Collections;

using IoTunas.Core.Collections;
using IoTunas.Extensions.Telemetry.Models.Reception;

public interface IMetaReceiverCollection : 
    IReadOnlyMetaReceiverCollection, 
    IMetaTypeCollection<MetaReceiver>
{

    bool Add(Type type, string inputName);

    bool Remove(string inputName);

}