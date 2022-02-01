using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp.Hosting
{
    class WpfHostedService<MainWindow> : IHostedService, IDisposable where MainWindow : System.Windows.Window
    {
        private readonly ILogger<WpfHostedService<MainWindow>> logger;
        private readonly IServiceProvider serviceProvider;

        public WpfHostedService(ILogger<WpfHostedService<MainWindow>> logger,
                                IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            logger.LogDebug("Created: " + GetHashCode().ToString());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug("StartAsync: " + GetHashCode().ToString());
            var shell = serviceProvider.GetService<MainWindow>();
            shell.Show();
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
