namespace IoTunas.Core.Builders.Containers;

using IoTunas.Core.Builders.ModuleClients;
using IoTunas.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public class IoTModuleBuilder : IoTContainerBuilderBase
{

    private Action<IModuleClientBuilder>? configureClientAction;
    private readonly List<Func<IServiceProvider, Task>> afterBuildActions;

    public IoTModuleBuilder(
        HostBuilderContext context,
        IServiceCollection services)
        : base(
              context.Configuration,
              context.HostingEnvironment,
              services)
    {
        afterBuildActions = new List<Func<IServiceProvider, Task>>();
    }

    public void ConfigureClient(Action<IModuleClientBuilder>? configureClientAction)
    {
        this.configureClientAction = configureClientAction;
    }

    public void AfterBuild(Func<IServiceProvider, Task> action)
    {
        afterBuildActions.Add(action);
    }

    public override IServiceProvider BuildServiceProvider()
    {

        // Register core services
        Services.AddSingleton<ModuleHostService>();
        Services.AddHostedService(provider =>
            provider.GetRequiredService<ModuleHostService>());
        Services.AddSingleton(provider =>
            provider.GetRequiredService<ModuleHostService>().Client);
        Services.AddTransient<IModuleClientBuilder>(provider =>
        {
            var builder = ModuleClientBuilder.FromEnvironment();
            configureClientAction?.Invoke(builder);
            return builder;
        });

        // Build service provider
        var provider = Services.BuildServiceProvider();
        var afterBuildTasks = new List<Task>();
        foreach(var action in afterBuildActions)
        {
            var task = action.Invoke(provider);
            afterBuildTasks.Add(task);
        }
        Task.WhenAll(afterBuildTasks).Wait();
        return provider;

    }

}
