namespace IoTunas.Extensions.Twin.Mediators;

public interface IReportedPropertiesMediator
{
    
    Task PushReportedPropertiesAsync(
        CancellationToken cancellationToken = default);

    Task PushReportedPropertyAsync(
        string propertyName,
        CancellationToken cancellationToken = default);

}
