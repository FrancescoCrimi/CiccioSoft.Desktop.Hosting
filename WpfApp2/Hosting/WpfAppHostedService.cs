// Copyright (c) 2023 - 2026 Francesco Crimi
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2.Hosting
{
    public class WpfAppHostedService<TWindow> : IHostedService, IDisposable where TWindow : Window
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public WpfAppHostedService(ILogger<WpfAppHostedService<TWindow>> logger,
                                   IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _logger.LogDebug("Created: {HashCode}", GetHashCode().ToString());
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("StartAsync: {HashCode}", GetHashCode().ToString());
            var shellWindow = _serviceProvider.GetService<TWindow>();
            shellWindow?.Show();
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("StopAsync: {HashCode}", GetHashCode().ToString());
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposed: {HashCode}", GetHashCode().ToString());
        }
    }
}
