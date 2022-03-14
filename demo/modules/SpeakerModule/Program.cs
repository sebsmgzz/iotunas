
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

    // Configure client
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

    // Add middleware
    module.UseCommandHandlers(builder => builder.MapHandlers());

})
.Build()
.Run();
