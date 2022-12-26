namespace IoTunas.Demos.Template.Services;

public interface ICounterService
{
    
    int Value { get; }

    int Increment();

    void Reset();

}
