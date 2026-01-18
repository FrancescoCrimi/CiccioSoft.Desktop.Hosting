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
using System.Windows.Forms;

namespace FormsApp2.Hosting
{
    public class FormHostLifetime : IHostLifetime, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public FormHostLifetime(ILogger<FormHostLifetime> logger,
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
            ApplicationConfiguration.Initialize();
            Application.ApplicationExit += Application_ApplicationExit;
            _logger.LogDebug("WaitForStartAsync: {HashCode}", GetHashCode().ToString());
            return Task.CompletedTask;
        }

        private void Application_ApplicationExit(object? sender, EventArgs e)
        {
            Application.ApplicationExit -= Application_ApplicationExit;
            _hostApplicationLifetime.StopApplication();
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposed: {HashCode}", GetHashCode().ToString());
        }
    }
}
