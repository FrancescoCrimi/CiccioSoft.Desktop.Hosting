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
        private readonly IServiceProvider serviceProvider;

        public FormHostedService(ILogger<FormHostedService<MainWindoww>> logger,
                                 IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            logger.LogDebug("Created: " + GetHashCode().ToString());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug("StartAsync: " + GetHashCode().ToString());
            ApplicationConfiguration.Initialize();
            var shell = serviceProvider.GetService<MainWindoww>();
            return Task.Run(()=> Application.Run(shell));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug("StopAsync: " + GetHashCode().ToString());
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            logger.LogDebug("Disposed: " + GetHashCode().ToString());
        }
    }
}
