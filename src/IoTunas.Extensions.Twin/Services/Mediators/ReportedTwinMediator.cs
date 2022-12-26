namespace IoTunas.Extensions.Twin.Services.Mediators;

using IoTunas.Core.Services.ClientHosts;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Threading.Tasks;

public class ReportedTwinMediator : IReportedTwinMediator
{

    public const string UpdateTookTooLongLog = 
        "Reported property {propertyName} update took too long." +
        "A new update is already available. Cancelling previous update.";

    private readonly IServiceProvider provider;
    private readonly ILogger logger;

    private Dictionary<string, CancellationTokenSource> tokenSources;
    private Twin? twin;

    public ReportedTwinMediator(
        IServiceProvider provider,
        ILogger<IReportedTwinMediator> logger)
    {
        this.provider = provider;
        this.logger = logger;
        tokenSources = new Dictionary<string, CancellationTokenSource>();
    }

    public void HandlePropertyChanging(object? sender, PropertyChangingEventArgs args)
    {
        // If property update took too long, cancel it and log a warning.
        // But, if property is been updated for the first time or
        // if previous cts had already been cancelled, create a new cts.
        var ctsExists = tokenSources.TryGetValue(args.PropertyName, out var cts);
        if (ctsExists && !cts.IsCancellationRequested)
        {
            logger.LogWarning(UpdateTookTooLongLog, args.PropertyName);
            cts.Cancel();
        }
        else
        {
            cts = new CancellationTokenSource();
            tokenSources.Add(args.PropertyName, cts);
        }
    }

    public void HandlePropertyChanged(object? sender, PropertyChangedEventArgs args)
    {

        // Get property update
        var propertyName = args.PropertyName!;
        var propertyInfo = sender!.GetType().GetProperty(propertyName);
        var propertyValue = propertyInfo!.GetValue(sender);

        // Send property update
        var cts = tokenSources[propertyName];
        var clientHost = provider.GetRequiredService<IIoTClientHost>();
        var token = cts!.Token;
        var updateTask = clientHost.IsEdgeCapable ?
            UpdateModuleClientProperties(propertyName, propertyValue, token) :
            UpdateDeviceClientProperties(propertyName, propertyValue, token);
        updateTask.Wait();
        cts.Cancel();

    }

    private async Task UpdateModuleClientProperties(
        string propertyName, 
        dynamic propertyValue,
        CancellationToken cancellationToken)
    {
        var client = provider.GetRequiredService<ModuleClient>();
        twin ??= await client.GetTwinAsync(cancellationToken);
        twin.Properties.Reported[propertyName] = propertyValue;
        client
            .UpdateReportedPropertiesAsync(twin.Properties.Reported, cancellationToken)
            .Wait(cancellationToken);
    }

    private async Task UpdateDeviceClientProperties(
        string propertyName,
        dynamic propertyValue,
        CancellationToken cancellationToken)
    {
        var client = provider.GetRequiredService<DeviceClient>();
        twin ??= await client.GetTwinAsync(cancellationToken);
        twin.Properties.Reported[propertyName] = propertyValue;
        client
            .UpdateReportedPropertiesAsync(twin.Properties.Reported, cancellationToken)
            .Wait(cancellationToken);
    }

}
