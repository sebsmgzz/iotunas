namespace IoTunas.Core.Services.ClientHosts;
using Microsoft.Extensions.Logging;
using System;

public class IoTHostBase
{

    public const string ClientOpenlog = "Client connection opened";
    public const string ClientCloselog = "Client connection closed";

    private readonly ILogger logger;
    protected readonly Lazy<CancellationTokenSource> clientOpened;
    protected readonly Lazy<CancellationTokenSource> clientClosed;

    public CancellationToken ClientOpened => clientOpened.Value.Token;

    public CancellationToken ClientClosed => clientClosed.Value.Token;

    public IoTHostBase(ILogger logger)
    {
        this.logger = logger;
        clientOpened = new Lazy<CancellationTokenSource>(CreateClientOpenedCts);
        clientClosed = new Lazy<CancellationTokenSource>(CreateClientClosedCts);
    }

    private CancellationTokenSource CreateClientOpenedCts()
    {
        var cts = new CancellationTokenSource();
        cts.Token.Register(() => logger.LogInformation(ClientOpenlog));
        return cts;
    }

    private CancellationTokenSource CreateClientClosedCts()
    {
        var cts = new CancellationTokenSource();
        cts.Token.Register(() => logger.LogInformation(ClientCloselog));
        return cts;
    }

}
