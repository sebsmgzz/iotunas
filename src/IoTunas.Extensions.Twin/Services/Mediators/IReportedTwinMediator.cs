namespace IoTunas.Extensions.Twin.Services.Mediators;
using System.ComponentModel;

public interface IReportedTwinMediator
{
    
    void HandlePropertyChanged(
        object? sender, 
        PropertyChangedEventArgs args);
    
    void HandlePropertyChanging(
        object? sender, 
        PropertyChangingEventArgs args);

}
