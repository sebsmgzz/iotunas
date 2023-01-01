namespace IoTunas.Demos.Template.Telemetry;

using IoTunas.Extensions.Telemetry.Emission;
using IoTunas.Extensions.Telemetry.Reflection;
using Microsoft.Azure.Devices.Client;

[TelemetryOutput("output1")]
public class Output1Provider : ITelemetryProvider<PipedTelemetry>
{

    public Message? Message { get; set; }

    public PipedTelemetry GetTelemetry()
    {
        return new PipedTelemetry(Message!); 
    }

}
