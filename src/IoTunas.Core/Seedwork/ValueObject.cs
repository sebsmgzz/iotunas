namespace IoTunas.Core.Seedwork;

public abstract class ValueObject
{

    protected abstract IEnumerable<object> GetEqualityComponents();

    public bool Equals(ValueObject? valueObject)
    {
        return valueObject != null && 
            GetEqualityComponents().SequenceEqual(
                valueObject.GetEqualityComponents());
    }

    public override bool Equals(object? obj)
    {
        return obj is ValueObject valueObject && Equals(valueObject);
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return left?.Equals(right) ?? right is null;
    }

    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left == right);
    }

}
