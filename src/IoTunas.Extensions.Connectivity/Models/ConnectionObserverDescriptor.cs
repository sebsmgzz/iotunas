namespace IoTunas.Extensions.Connectivity.Models;

public class ConnectionObserverDescriptor
{

    private Type? _type;

    public Type Type
    {
        get => _type!;
        private set
        {
            var interfaceType = typeof(IConnectionObserver);
            if (!value.IsAssignableTo(interfaceType))
            {
                throw new InvalidOperationException(
                    $"Type {value.Name} must implement {interfaceType.Name} " +
                    $"in order to be assigned as type in a {nameof(ConnectionObserverDescriptor)}.");
            }
            _type = value;
        }
    }

    public ConnectionObserverDescriptor(Type type)
    {
        Type = type;
    }

    public bool Equals(ConnectionObserverDescriptor? descriptor)
    {
        return descriptor != null && descriptor.Type.Equals(Type);
    }

    public override bool Equals(object? obj)
    {
        return obj is ConnectionObserverDescriptor descriptor && Equals(descriptor);
    }

    public override int GetHashCode()
    {
        return Type.GetHashCode();
    }

    public override string ToString()
    {
        return $"[{nameof(ConnectionObserverDescriptor)}: {Type}]";
    }

    public static bool operator ==(ConnectionObserverDescriptor? left, ConnectionObserverDescriptor? right)
    {
        return left?.Equals(right) ?? right is null;
    }

    public static bool operator !=(ConnectionObserverDescriptor? left, ConnectionObserverDescriptor? right)
    {
        return !(left == right);
    }

}