namespace IoTunas.Extensions.Methods.Models;

public class CommandDescriptor
{

    private Type? _type;

    public Type Type
    {
        get => _type!;
        private set
        {
            var interfaceType = typeof(ICommand);
            if (!value.IsAssignableTo(interfaceType))
            {
                throw new InvalidOperationException(
                    $"Type {value.Name} must implement {interfaceType.Name} " +
                    $"in order to be assigned as type in a {nameof(CommandDescriptor)}.");
            }
            _type = value;
        }
    }

    public string MethodName { get; }

    public CommandDescriptor(Type type, string methodName)
    {
        Type = type;
        MethodName = methodName;
    }

    public bool Equals(CommandDescriptor? descriptor)
    {
        return descriptor != null && descriptor.MethodName.Equals(MethodName);
    }

    public override bool Equals(object? obj)
    {
        return obj is CommandDescriptor descriptor && Equals(descriptor);
    }

    public override int GetHashCode()
    {
        return MethodName.GetHashCode();
    }

    public override string ToString()
    {
        return $"[{nameof(CommandDescriptor)}: {MethodName}]";
    }

    public static bool operator ==(CommandDescriptor? left, CommandDescriptor? right)
    {
        return left?.Equals(right) ?? right is null;
    }

    public static bool operator !=(CommandDescriptor? left, CommandDescriptor? right)
    {
        return !(left == right);
    }

}