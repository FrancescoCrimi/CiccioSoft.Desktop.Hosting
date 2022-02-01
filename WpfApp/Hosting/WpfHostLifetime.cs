using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp.Hosting
{
    public class WpfHostLifetime : IHostLifetime, IDisposable
    {
        private readonly ILogger<WpfHostLifetime> logger;
        private readonly IHostApplicationLifetime hostApplicationLifetime;

        public WpfHostLifetime(ILogger<WpfHostLifetime> logger,
                               IHostApplicationLifetime hostApplicationLifetime)
        {
            this.logger = logger;
            this.hostApplicationLifetime = hostApplicationLifetime;
            logger.LogDebug("Created: " + GetHashCode().ToString());
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug("StopAsync: " + GetHashCode().ToString());
            return Task.CompletedTask;
        }

        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            App.Current.Exit += ApplicationExit;
            logger.LogDebug("WaitForStartAsync: " + GetHashCode().ToString());
            return Task.CompletedTask;
        }

        private void ApplicationExit(object sender, ExitEventArgs e)
        {
            App.Current.Exit -= ApplicationExit;
            hostApplicationLifetime.StopApplication();
        }

        public void Dispose()
        {
            logger.LogDebug("Disposed: " + GetHashCode().ToString());
        }
    }
}
