namespace IoTunas.Extensions.Methods.Collections;

using IoTunas.Core.Collections;
using IoTunas.Extensions.Methods.Models.Commands;
using System.Collections;
using System.Collections.Generic;

public class MetaCommandCollection : MetaTypeCollection<MetaCommand>, IMetaCommandCollection
{

    public override MetaCommand? Get(Type type)
    {
        return items.FirstOrDefault(command => command?.Type.Equals(type) ?? false, null);
    }

    public override bool Add(Type type)
    {
        return Add(new MetaCommand(type));
    }

    public bool Add(Type type, string methodName)
    {
        var directMethod = new DirectMethod(methodName);
        return Add(new MetaCommand(type, directMethod));
    }

    public bool Add<TType>(string methodName) where TType : ICommand
    {
        return Add(typeof(TType), methodName);
    }

    public bool Remove(string methodName)
    {
        var removedCount = items.RemoveWhere(command => command.Method.Name == methodName);
        return removedCount > 0;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        var enumerable = (IEnumerable)items;
        return enumerable.GetEnumerator();
    }

    public IReadOnlyDictionary<string, Type> AsMapping()
    {
        var mapping = new Dictionary<string, Type>();
        foreach(var command in this)
        {
            mapping.Add(command.Method.Name, command.Type);
        }
        return mapping;
    }

}
