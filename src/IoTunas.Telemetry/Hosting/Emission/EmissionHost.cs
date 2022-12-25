namespace IoTunas.Extensions.Telemetry.Hosting.Emission;

using IoTunas.Extensions.Telemetry.Factories;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

public class EmissionHost : IHostedService
{

    private readonly IEmissaryControllerFactory factory;

    public EmissionHost(IEmissaryControllerFactory factory)
    {
        this.factory = factory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        foreach(var controller in factory.GetAll())
        {
            if(controller.Descriptor.AutoStart)
            {
                controller.Start();
            }
        }
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var controller in factory.GetAll())
        {
            controller.Stop(force: true);
        }
        return Task.CompletedTask;
    }

}
