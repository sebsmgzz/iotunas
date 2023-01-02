﻿namespace IoTunas.Extensions.Methods.Commands;

using Microsoft.Azure.Devices.Client;

public interface IMethodResponseFactory
{

    MethodResponse BadRequest(object? payload = null);

    MethodResponse BrewCoffe(object? payload = null);

    MethodResponse InternalError(object? payload = null);

    MethodResponse NotFound(object? payload = null);

    MethodResponse Ok(object? payload = null);

    MethodResponse Conflict(object? payload = null);

}