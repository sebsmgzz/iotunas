namespace IoTunas.Extensions.Telemetry.Models;

using Microsoft.Azure.Devices.Client;
using System.Threading.Tasks;

public interface IInputTelemetryBroker
{

    Task<MessageResponse> ReceiveAsync(Message message, object userContext);

}
