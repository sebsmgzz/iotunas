namespace IoTunas.Extensions.Twin.Models;

using System.ComponentModel;
using System.Runtime.CompilerServices;

public abstract class ReportedTwinModel : IReportedTwinModel, INotifyPropertyChanging, INotifyPropertyChanged
{
    
    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        var args = new PropertyChangedEventArgs(propertyName);
        PropertyChanged?.Invoke(this, args);
    }

    protected void NotifyPropertyChanging([CallerMemberName] string propertyName = "")
    {
        var args = new PropertyChangingEventArgs(propertyName);
        PropertyChanging?.Invoke(this, args);
    }

    protected bool SetProperty<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = "")
    {
        var isDifferent = !EqualityComparer<T>.Default.Equals(oldValue, newValue);
        if (isDifferent)
        {
            NotifyPropertyChanging(propertyName);
            oldValue = newValue;
            NotifyPropertyChanged(propertyName);
        }
        return isDifferent;
    }

}
