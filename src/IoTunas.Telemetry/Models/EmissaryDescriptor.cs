namespace IoTunas.Extensions.Telemetry.Models;

public class EmissaryDescriptor
{

    public const bool DefaultAutoStart = true;
    public readonly static TimeSpan DefaultPeriod = TimeSpan.FromMinutes(1);

    private Type? _type;

    public Type Type
    {
        get => _type!;
        private set
        {
            var interfaceType = typeof(IEmissary);
            if (!value.IsAssignableTo(interfaceType))
            {
                throw new InvalidOperationException(
                    $"Type {value.Name} must implement {interfaceType.Name} " +
                    $"in order to be assigned as type in a {nameof(EmissaryDescriptor)}.");
            }
            _type = value;
        }
    }

    public string OutputName { get; }

    public TimeSpan InitialPeriod { get; }

    public bool AutoStart { get; }

    public EmissaryDescriptor(Type type, string outputName)
        : this(type, outputName, DefaultPeriod, DefaultAutoStart)
    {
    }

    public EmissaryDescriptor(Type type, string outputName, TimeSpan initialPeriod)
        : this(type, outputName, initialPeriod, DefaultAutoStart)
    {
    }

    public EmissaryDescriptor(Type type, string outputName, bool autoStart)
        : this(type, outputName, DefaultPeriod, autoStart)
    {
    }

    public EmissaryDescriptor(Type type, string outputName, TimeSpan initialPeriod, bool autoStart)
    {
        Type = type;
        OutputName = outputName;
        InitialPeriod = initialPeriod;
        AutoStart = autoStart;
    }

    public bool Equals(EmissaryDescriptor? descriptor)
    {
        return descriptor != null && descriptor.OutputName.Equals(OutputName);
    }

    public override bool Equals(object? obj)
    {
        return obj is EmissaryDescriptor descriptor && Equals(descriptor);
    }

    public override int GetHashCode()
    {
        return OutputName.GetHashCode();
    }

    public override string ToString()
    {
        return $"[{nameof(EmissaryDescriptor)}: {OutputName}]";
    }

    public static bool operator ==(EmissaryDescriptor? left, EmissaryDescriptor? right)
    {
        return left?.Equals(right) ?? right is null;
    }

    public static bool operator !=(EmissaryDescriptor? left, EmissaryDescriptor? right)
    {
        return !(left == right);
    }

}
