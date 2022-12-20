namespace IoTunas.Extensions.Commands.Hosting;

using IoTunas.Extensions.Commands.Mediators;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

public class DeviceCommandHandlingService : IHostedService
{

    private readonly IServiceProvider serviceProvider;

    public DeviceCommandHandlingService(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var client = serviceProvider.GetRequiredService<DeviceClient>();
        var mediator = serviceProvider.GetRequiredService<ICommandHandlerMediator>();
        await client.SetMethodDefaultHandlerAsync(mediator.HandleAsync, this, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var client = serviceProvider.GetRequiredService<DeviceClient>();
        await client.SetMethodDefaultHandlerAsync(null, null, cancellationToken);
    }

}
