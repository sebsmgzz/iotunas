
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Configuration;
using IoTunas.Core.Hosting;
using IoTunas.Extensions.Commands;
using IoTunas.Extensions.Connectivity;
using IoTunas.Extensions.Telemetry;
using IoTunas.Extensions.Twin;

Host
.CreateDefaultBuilder(args)
.ConfigureIoTModuleDefaults(module =>
{
    module.ConfigureClient(client =>
    {
        client.TransportSettings.AddAmqp(TransportType.Amqp);
        if (module.Environment.IsDevelopment())
        {
            client.UseConnectionString(
                module.Configuration.GetConnectionString("DevModuleConnStr"));
        }
        else
        {
            client.UseEnvironment();
        }
    });
    module.UseCommandHandlers(builder => builder.MapHandlers());
    module.UseTelemetryInputs(builder => builder.MapReceivers());
    module.UseConnectivityServices(builder => builder.MapObservers());
    module.UseTwinDesiredProperties(builder => builder.MapModels());
    module.UseTwinReportedProperties(builder => builder.MapModels());
})
.Build()
.Run();
