namespace IoTunas.Extensions.Telemetry.Collections;

using IoTunas.Extensions.Telemetry.Models.Reception;
using IoTunas.Core.Collections;
using System;
using System.Collections.Generic;

public class MetaReceiverCollection : MetaTypeCollection<MetaReceiver>, IMetaReceiverCollection
{

    public override MetaReceiver? Get(Type type)
    {
        return items.FirstOrDefault(receiver => receiver?.Type.Equals(type) ?? false, null);
    }

    public override bool Add(Type type)
    {
        return Add(new MetaReceiver(type));
    }

    public bool Add(Type type, string inputName)
    {
        var input = new TelemetryInput(inputName);
        return Add(new MetaReceiver(type, input));
    }

    public bool Remove(string inputName)
    {
        var removedCount = items.RemoveWhere(receiver => receiver.Input.Name == inputName);
        return removedCount > 0;
    }

    public IReadOnlyDictionary<string, Type> AsMapping()
    {
        var mapping = new Dictionary<string, Type>();
        foreach (var receiver in this)
        {
            mapping.Add(receiver.Input.Name, receiver.Type);
        }
        return mapping;
    }

}
