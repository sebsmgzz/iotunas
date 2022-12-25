namespace IoTunas.Extensions.Telemetry.Models;

public class ReceiverDescriptor
{

    private Type? _type;

    public Type Type
    {
        get => _type!;
        private set
        {
            var interfaceType = typeof(IReceiver);
            if (!value.IsAssignableTo(interfaceType))
            {
                throw new InvalidOperationException(
                    $"Type {value.Name} must implement {interfaceType.Name} " +
                    $"in order to be assigned as type in a {nameof(ReceiverDescriptor)}.");
            }
            _type = value;
        }
    }

    public string InputName { get; }

    public ReceiverDescriptor(Type type, string inputName)
    {
        Type = type;
        InputName = inputName;
    }

    public bool Equals(ReceiverDescriptor? descriptor)
    {
        return descriptor != null && descriptor.InputName.Equals(InputName);
    }

    public override bool Equals(object? obj)
    {
        return obj is ReceiverDescriptor descriptor && Equals(descriptor);
    }

    public override int GetHashCode()
    {
        return InputName.GetHashCode();
    }

    public override string ToString()
    {
        return $"[{nameof(ReceiverDescriptor)}: {InputName}]";
    }

    public static bool operator ==(ReceiverDescriptor? left, ReceiverDescriptor? right)
    {
        return left?.Equals(right) ?? right is null;
    }

    public static bool operator !=(ReceiverDescriptor? left, ReceiverDescriptor? right)
    {
        return !(left == right);
    }

}
