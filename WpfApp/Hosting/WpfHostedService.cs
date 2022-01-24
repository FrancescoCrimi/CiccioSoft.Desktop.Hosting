using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp.Hosting
{
    class WpfHostedService<MainWindoww> : IHostedService, IDisposable where MainWindoww : System.Windows.Window
    {
        private readonly ILogger<WpfHostedService<MainWindoww>> logger;
        private readonly IHostApplicationLifetime hostApplicationLifetime;
        private readonly IServiceProvider serviceProvider;

        public WpfHostedService(ILogger<WpfHostedService<MainWindoww>> logger,
                                IHostApplicationLifetime hostApplicationLifetime,
                                IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.hostApplicationLifetime = hostApplicationLifetime;
            this.serviceProvider = serviceProvider;
            logger.LogDebug("Created: " + GetHashCode().ToString());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            App.Current.Exit += ApplicationExit;
            serviceProvider.GetService<MainWindoww>().Show();
            logger.LogDebug("StartAsync: " + GetHashCode().ToString());
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug("StopAsync: " + GetHashCode().ToString());
            return Task.CompletedTask;
        }

        private void ApplicationExit(object sender, ExitEventArgs e)
        {
            App.Current.Exit -= ApplicationExit;
            hostApplicationLifetime.StopApplication();
        }

        public void Dispose()
        {
            logger.LogDebug("Disposed" + GetHashCode().ToString());
        }
    }
}
