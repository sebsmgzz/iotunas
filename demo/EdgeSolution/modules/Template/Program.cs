using IoTunas.Demos.Template.Services;
using IoTunas.Core.Hosting;
using IoTunas.Extensions.Methods.Hosting;
using IoTunas.Extensions.Connectivity.Hosting;
using IoTunas.Extensions.Telemetry.Hosting;
using Microsoft.Azure.Devices.Client;

Host
.CreateDefaultBuilder(args)
.ConfigureIoTModuleDefaults(builder =>
{

    // Configure client
    builder.Client.Transports.AddMqtt(TransportType.Mqtt_Tcp_Only);

    // Configure extensions
    builder.UseCommands();
    builder.UseConnectionObservers();
    builder.UseTelemetryEmission();
    builder.UseTelemetryReception();

})
.ConfigureServices(services => 
{
    services.AddSingleton<ICounterService, CounterService>();
})
.Build()
.Run();
