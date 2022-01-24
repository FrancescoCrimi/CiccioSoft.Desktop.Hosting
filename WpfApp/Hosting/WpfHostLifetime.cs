using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp.Hosting
{
    public class WpfHostLifetime : IHostLifetime, IDisposable
    {
        private readonly ILogger<WpfHostLifetime> logger;

        public WpfHostLifetime(ILogger<WpfHostLifetime> logger)
        {
            this.logger = logger;
            logger.LogDebug("Created: " + GetHashCode().ToString());
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug("StopAsync: " + GetHashCode().ToString());
            return Task.CompletedTask;
        }

        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug("WaitForStartAsync: " + GetHashCode().ToString());
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            logger.LogDebug("Disposed: " + GetHashCode().ToString());
        }
    }
}
