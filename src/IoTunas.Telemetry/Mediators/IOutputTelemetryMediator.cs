namespace IoTunas.Extensions.Telemetry.Mediators;

using Microsoft.Azure.Devices.Client;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public interface IOutputTelemetryMediator
{

    Task SendAsync(
        byte[] messageBytes,
        string? outputName = null,
        Action<Message>? configureMessage = null,
        CancellationToken cancellation = default);

    Task SendAsync(
        Message message,
        string? outputName = null,
        CancellationToken cancellation = default);

    Task SendAsync(
        object messageObject,
        string? outputName = null,
        Action<Message>? configureMessage = null,
        CancellationToken cancellation = default);

    Task SendAsync(
        Stream messageStream,
        string? outputName = null,
        Action<Message>? configureMessage = null,
        CancellationToken cancellation = default);

    Task SendAsync(
        string messageString,
        string? outputName = null,
        Action<Message>? configureMessage = null,
        CancellationToken cancellation = default);

}