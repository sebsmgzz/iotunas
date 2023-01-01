using IoTunas.Core.Hosting;
using IoTunas.Extensions.Methods.Hosting;
using IoTunas.Extensions.Connectivity.Hosting;
using IoTunas.Extensions.Telemetry.Hosting;
using Microsoft.Azure.Devices.Client;
using IoTunas.Demos.GuidsModule.Services.Guids;

Host
.CreateDefaultBuilder(args)
.ConfigureIoTModuleDefaults(builder =>
{

    // Configure client
    builder.Client.Transports.AddMqtt(TransportType.Mqtt_Tcp_Only);

    // Configure extensions
    builder.UseCommands(builder =>
    {

    });
    builder.UseConnectionObservers();
    builder.MapTelemetryServices();

})
.ConfigureServices(services =>
{
    services.AddScoped<IGuidProvider, GuidProvider>();
    services.AddSingleton<ISingletonGuidGenerator, GuidGenerator>();
    services.AddScoped<IScopedGuidGenerator, GuidGenerator>();
    services.AddTransient<ITransientGuidGenerator, GuidGenerator>();
})
.Build()
.Run();
