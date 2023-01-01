namespace IoTunas.Extensions.Telemetry.Emission;

using IoTunas.Extensions.Telemetry.Models.Emission;

// The entire idea for making this interface generic is ensure a 1 to 1 relationship
// between the telemetry models and the telemetry providers.
// Unfortunately, this also causes intense reflection ussage in other areas of the application.
// Should we remove the generic argument from the interface and instead use some sort of
// ITelemetryProviderFactory to ensure this 1:1 relationship?
// How will we differentiate between telemetry providers then?
// Can/Should different providers, provide the same type of telemetry model?
// Can/Should a single provider, provide different types of telemetry models?
// In other words:
// Should we ensure that a single IoT route delivers telemetry/messages
// using the same schema each time, or should we allow the schema to vary?
// IMO: The schema should be allowed to vary, but best practice is to keep a consistent
// schema for a each IoT route, then the question becomes: Should enforce this best practice?
// TODO: Open discussion about this and remove this long comment
public interface ITelemetryProvider<out TTelemetry> where TTelemetry : ITelemetry
{

    internal Type ControllerImplementation => typeof(TelemetryController<ITelemetry>);

    internal Type ControllerAbstraction => typeof(ITelemetryController<ITelemetry>);

    TTelemetry GetTelemetry();

}
