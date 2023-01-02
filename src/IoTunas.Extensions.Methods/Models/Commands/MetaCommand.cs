namespace IoTunas.Extensions.Methods.Models.Commands;

using IoTunas.Core.Seedwork;
using IoTunas.Extensions.Methods.Reflection;

public class MetaCommand
{

    public InheritedType<ICommand> Type { get; }

    public DirectMethod Method { get; }

    public MetaCommand(Type type) : this(type, DirectMethodAttribute.GetDirectMethodOrDefault(type))
    {
    }

    public MetaCommand(Type type, DirectMethod method)
    {
        Type = type;
        Method = method;
    }

}
