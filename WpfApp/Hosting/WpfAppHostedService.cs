using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp.Hosting
{
    class WpfAppHostedService<TWindow> : IHostedService, IDisposable where TWindow : System.Windows.Window
    {
        private readonly ILogger<WpfAppHostedService<TWindow>> logger;
        private readonly IServiceProvider serviceProvider;

        public WpfAppHostedService(ILogger<WpfAppHostedService<TWindow>> logger,
                                   IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            logger.LogDebug("Created: " + GetHashCode().ToString());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            serviceProvider.GetService<TWindow>().Show();
            logger.LogDebug("StartAsync: " + GetHashCode().ToString());
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug("StopAsync: " + GetHashCode().ToString());
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            logger.LogDebug("Disposed" + GetHashCode().ToString());
        }
    }
}
