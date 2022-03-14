namespace CameraModule.Services;

using CameraModule.Services.ImageCapture;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public class Engine : IHostedService, IDisposable
{

    private readonly IImageCaptureService imageCapture;
    private readonly ILogger logger;
    private readonly Timer timer;
    private readonly AutoResetEvent autoResetEvent;
    private readonly Thread thread;
    private readonly CancellationTokenSource cts;

    public Engine(
        IImageCaptureService imageCapture,
        ILogger<Engine> logger)
    {
        this.imageCapture = imageCapture;
        this.logger = logger;
        timer = new Timer(HandleTimerEvent, null, 0, 10000);
        autoResetEvent = new AutoResetEvent(false);
        thread = new Thread(HandleThreadStart);
        cts = new CancellationTokenSource();
    }

    private void HandleTimerEvent(object state)
    {
        autoResetEvent.Set();
    }

    private void HandleThreadStart()
    {
        while(!cts.IsCancellationRequested)
        {
            if(autoResetEvent.WaitOne())
            {
                imageCapture.CaptureImageAsync(cts.Token).Wait();
                timer.Change(0, 10000);
                autoResetEvent.Reset();
            }
        }
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        thread.Start();
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        autoResetEvent.Set(); // Release thread if still waiting
        cts.Cancel();
        await Task.CompletedTask;
    }

    public void Dispose()
    {
        autoResetEvent.Dispose();
        timer.Dispose();
    }

}
