namespace IoTunas.Extensions.Commands.Collections;

using IoTunas.Core.Hosting;
using IoTunas.Extensions.Commands.Models;
using IoTunas.Extensions.Commands.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class CommandServiceCollection : IEnumerable<CommandDescriptor>
{

    private readonly HashSet<CommandDescriptor> descriptors;

    public int Count => descriptors.Count;

    public CommandServiceCollection()
    {
        descriptors = new HashSet<CommandDescriptor>();
    }

    public CommandDescriptor Add(CommandDescriptor descriptor)
    {
        return descriptors.Add(descriptor) ? descriptor : descriptors.First(d => d.Equals(descriptor));
    }

    public CommandDescriptor Add(Type type)
    {
        var descriptor = CommandDescriptorAttribute.GetDescriptor(type);
        return Add(descriptor);
    }

    public CommandDescriptor Add<TType>()
    {
        return Add(typeof(TType));
    }

    public CommandDescriptor Add(Type type, string methodName)
    {
        var descriptor = new CommandDescriptor(type, methodName);
        return Add(descriptor);
    }

    public CommandDescriptor Add<TType>(string methodName)
    {
        var descriptor = new CommandDescriptor(typeof(TType), methodName);
        return Add(descriptor);
    }

    public void Map(Assembly assembly)
    {
        foreach (var type in assembly.GetDerivedTypes<ICommand>())
        {
            Add(type);
        }
    }

    public void Map()
    {
        var assembly = Assembly.GetEntryAssembly();
        Map(assembly!);
    }

    public IEnumerator<CommandDescriptor> GetEnumerator()
    {
        var enumerable = (IEnumerable<CommandDescriptor>)descriptors;
        return enumerable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        var enumerable = (IEnumerable)descriptors;
        return enumerable.GetEnumerator();
    }

}
