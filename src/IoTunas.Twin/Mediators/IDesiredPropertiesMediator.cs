namespace IoTunas.Twin.Mediators;

using System.Threading;
using System.Threading.Tasks;

public interface IDesiredPropertiesMediator
{
    Task PullDesiredPropertiesAsync(CancellationToken cancellationToken = default);
}