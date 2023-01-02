namespace IoTunas.Core.Seedwork;

using System.Collections.Generic;

public class InheritedType<TBase> : ValueObject
{

    public Type Value { get; }

    public Type BaseType => typeof(TBase);

    public InheritedType(Type value)
    {
        Value = Validate(value);
    }

    public static bool IsValid(Type? value)
    {
        var baseType = typeof(TBase);
        return value?.IsAssignableTo(baseType) ?? false;
    }

    private Type Validate(Type type)
    {
        if (!IsValid(type))
        {
            throw new InvalidOperationException(
                $"Type {type.Name} must implement or extend {BaseType.Name} " +
                $"in order to be assigned as an inherited type.");
        }
        return type;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return BaseType;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator Type(InheritedType<TBase> attribute)
    {
        return attribute.Value;
    }

    public static implicit operator InheritedType<TBase>(Type type)
    {
        return new InheritedType<TBase>(type);
    }

}
