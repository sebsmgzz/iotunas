namespace IoTunas.Core.Builders.Containers;

using IoTunas.Core.Builders.DeviceClients;
using IoTunas.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public class IoTDeviceBuilder : IoTContainerBuilderBase
{

    private Action<IDeviceClientBuilder>? configureClientAction;
    private readonly List<Func<IServiceProvider, Task>> afterBuildActions;

    public IoTDeviceBuilder(
        HostBuilderContext context,
        IServiceCollection services)
        : base(
              context.Configuration,
              context.HostingEnvironment,
              services)
    {
        afterBuildActions = new List<Func<IServiceProvider, Task>>();
    }

    public void ConfigureClient(Action<IDeviceClientBuilder>? configureClientAction)
    {
        this.configureClientAction = configureClientAction;
    }

    public void AfterBuild(Func<IServiceProvider, Task> action)
    {
        afterBuildActions.Add(action);
    }

    public override IServiceProvider BuildServiceProvider()
    {

        // Add core services
        Services.AddSingleton<DeviceHostService>();
        Services.AddSingleton(provider =>
            provider.GetRequiredService<DeviceHostService>().Client);
        Services.AddHostedService(provider =>
            provider.GetRequiredService<DeviceHostService>());
        Services.AddTransient<IDeviceClientBuilder>(provider =>
        {
            var config = provider.GetRequiredService<IConfiguration>();
            var builder = DeviceClientBuilder.FromConnectionString(
                config.GetConnectionString("Default"));
            configureClientAction?.Invoke(builder);
            return builder;
        });

        // Add post build actions
        var provider = Services.BuildServiceProvider();
        var tasks = new List<Task>();
        foreach(var action in afterBuildActions)
        {
            var task = action.Invoke(provider);
            tasks.Add(task);
        }
        Task.WhenAll(tasks).Wait();
        return provider;

    }

}
