// Copyright (c) 2023 - 2026 Francesco Crimi
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp2.Hosting
{
    public class WpfAppLifetime : IHostLifetime, IDisposable
    {
        private readonly ILogger<WpfAppLifetime> _logger;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public WpfAppLifetime(ILogger<WpfAppLifetime> logger,
                              IHostApplicationLifetime hostApplicationLifetime)
        {
            _logger = logger;
            _hostApplicationLifetime = hostApplicationLifetime;
            _logger.LogDebug("Created: {HashCode}", GetHashCode().ToString());
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("StopAsync: {HashCode}", GetHashCode().ToString());
            return Task.CompletedTask;
        }

        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("WaitForStartAsync: {HashCode}", GetHashCode().ToString());
            System.Windows.Application.Current.Exit += ApplicationExit;
            return Task.CompletedTask;
        }

        private void ApplicationExit(object sender, System.Windows.ExitEventArgs e)
        {
            System.Windows.Application.Current.Exit -= ApplicationExit;
            _hostApplicationLifetime.StopApplication();
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposed: {HashCode}", GetHashCode().ToString());
        }
    }
}
