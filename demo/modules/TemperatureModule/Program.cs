
using Microsoft.Extensions.Hosting;
using IoTunas.Core;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Configuration;

Host.CreateDefaultBuilder(args)
.ConfigureIoTEdgeDefaults(iotEdge =>
{
    iotEdge.ConfigureClient(builder =>
    {
        builder.TransportSettings.AddAmqp(TransportType.Amqp);
        if(iotEdge.Environment.IsDevelopment())
        {
            var connectionString = iotEdge.Configuration.GetConnectionString("DevModuleConnStr");
            builder.UseConnectionString(connectionString);
        }
        else
        {
            builder.UseEnvironment();
        }
    });
    iotEdge.UseDirectMethodServices(directMethods =>
    {
        directMethods.AddHandlers(map => map.MapHandlers());
    });
    iotEdge.UseTelemetryServices(telemetry => telemetry.AddOutputBrokers());
    iotEdge.UseConnectivityServices(connectivity =>
    {
        connectivity.AddObservers(map =>
        {
        });
    });
    iotEdge.UseTwinServices(twin =>
    {
        twin.AddDesiredProperties(map =>
        {

        });
        twin.AddReportedProperties(map =>
        {

        });
    });
})
.Build()
.Run();
