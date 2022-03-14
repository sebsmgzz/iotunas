
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Configuration;
using IoTunas.Core.Hosting;
using IoTunas.Extensions.Commands;
using IoTunas.Extensions.Connectivity;
using IoTunas.Extensions.Telemetry;
using IoTunas.Extensions.Twin;
using CameraModule.Services;
using Microsoft.Extensions.DependencyInjection;
using CameraModule.Services.ImageCapture;

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

    // Add services
    module.Services.AddHostedService<Engine>();
    module.Services.AddSingleton<IImageCaptureService, ImageCaptureService>();

    // Add middleware
    module.UseCommandHandlers(builder => builder.MapHandlers());
    module.UseTelemetryInputs(builder => builder.MapReceivers());
    module.UseConnectivityServices(builder => builder.MapObservers());
    module.UseTwinDesiredProperties(builder => builder.MapModels());
    module.UseTwinReportedProperties(builder => builder.MapModels());

})
.Build()
.Run();
