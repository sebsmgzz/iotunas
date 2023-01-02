namespace IoTunas.Extensions.Telemetry.Collections;

using IoTunas.Core.Collections;
using IoTunas.Extensions.Telemetry.Models.Reception;
using System;
using System.Collections.Generic;

public interface IReadOnlyMetaReceiverCollection : 
    IReadOnlyMetaTypeCollection<MetaReceiver>,
    IEnumerable<MetaReceiver>
{

    IReadOnlyDictionary<string, Type> AsMapping();

}
