using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormApp.Hosting
{
    public class FormHostedService<MainWindoww> : IHostedService, IDisposable where MainWindoww : Form
    {
        private readonly ILogger<FormHostedService<MainWindoww>> logger;
        private readonly IHostApplicationLifetime hostApplicationLifetime;
        private readonly IServiceProvider serviceProvider;

        public FormHostedService(ILogger<FormHostedService<MainWindoww>> logger,
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
            Application.ApplicationExit += Application_ApplicationExit;
            logger.LogDebug("StartAsync: " + GetHashCode().ToString());
            ApplicationConfiguration.Initialize();
            var shell = serviceProvider.GetService<MainWindoww>();
            Application.Run(shell);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug("StopAsync: " + GetHashCode().ToString());
            return Task.CompletedTask;
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            Application.ApplicationExit -= Application_ApplicationExit;
            hostApplicationLifetime.StopApplication();
        }

        public void Dispose()
        {
            logger.LogDebug("Disposed: " + GetHashCode().ToString());
        }
    }
}
