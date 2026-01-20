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

namespace WpfApp1
{
    public class WpfHostLifetime : IHostLifetime
    {
        private readonly ILogger _logger;
        private readonly IHostEnvironment _environment;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private CancellationTokenRegistration _applicationStartedRegistration;
        private CancellationTokenRegistration _applicationStoppingRegistration;

        public WpfHostLifetime(ILoggerFactory loggerFactory,
                               IHostEnvironment environment,
                               IHostApplicationLifetime applicationLifetime)
        {
            _logger = loggerFactory.CreateLogger("Microsoft.Hosting.Lifetime");
            _environment = environment;
            _applicationLifetime = applicationLifetime;
        }

        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            _applicationStartedRegistration = _applicationLifetime.ApplicationStarted.Register(state =>
            {
                ((WpfHostLifetime)state!).OnApplicationStarted();
            },
            this);
            _applicationStoppingRegistration = _applicationLifetime.ApplicationStopping.Register(state =>
            {
                ((WpfHostLifetime)state!).OnApplicationStopping();
            },
            this);

            RegisterShutdownHandlers();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void OnApplicationStarted()
        {
            _logger.LogInformation("Wpf Application started.");
            _logger.LogInformation("Hosting environment: {envName}", _environment.EnvironmentName);
            _logger.LogInformation("Content root path: {contentRoot}", _environment.ContentRootPath);
        }

        private void OnApplicationStopping()
        {
            _logger.LogInformation("Wpf Application is shutting down...");
        }

        private void RegisterShutdownHandlers()
        {
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
        }

        private void OnProcessExit(object? sender, EventArgs e)
        {
            _applicationLifetime.StopApplication();

            UnregisterShutdownHandlers();
            _applicationStartedRegistration.Dispose();
            _applicationStoppingRegistration.Dispose();
        }

        private void UnregisterShutdownHandlers()
        {
            AppDomain.CurrentDomain.ProcessExit -= OnProcessExit;
        }
    }
}
