namespace IoTunas.Demos.Template.Services;

public class CounterService : ICounterService
{

    private int _count;

    public int Value => _count;

    public int Increment()
    {
        return Interlocked.Increment(ref _count);
    }

    public void Reset()
    {
        _count = 0;
    }

}
