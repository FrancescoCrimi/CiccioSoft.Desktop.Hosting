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
using System.Windows.Forms;

namespace FormApp
{
    public class FormHostLifetime : IHostLifetime, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IHostEnvironment _environment;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly IServiceProvider _serviceProvider;
        private CancellationTokenRegistration applicationStartedRegistration;
        private CancellationTokenRegistration applicationStoppingRegistration;

        public FormHostLifetime(ILoggerFactory loggerFactory,
                                IHostEnvironment environment,
                                IHostApplicationLifetime applicationLifetime,
                                IServiceProvider serviceProvider)
        {
            _logger = loggerFactory.CreateLogger("Microsoft.Hosting.Lifetime");
            _environment = environment;
            _applicationLifetime = applicationLifetime;
            _serviceProvider = serviceProvider;
        }

        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            applicationStartedRegistration = _applicationLifetime.ApplicationStarted.Register(state =>
            {
                ((FormHostLifetime)state!).OnApplicationStarted();
            },
            this);
            applicationStoppingRegistration = _applicationLifetime.ApplicationStopping.Register(state =>
            {
                ((FormHostLifetime)state!).OnApplicationStopping();
            },
            this);

            RegisterShutdownHandlers();

            var threadForm = new System.Threading.Thread(StartForm);
            threadForm.SetApartmentState(System.Threading.ApartmentState.STA);
            threadForm.Name = "StartForm Thread";
            threadForm.Start();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void OnApplicationStarted()
        {
            _logger.LogInformation("Application started.");
            _logger.LogInformation("Hosting environment: {envName}", _environment.EnvironmentName);
            _logger.LogInformation("Content root path: {contentRoot}", _environment.ContentRootPath);
        }

        private void OnApplicationStopping()
        {
            _logger.LogInformation("Application is shutting down...");
        }

        private void RegisterShutdownHandlers()
        {
            //AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            Application.ApplicationExit += OnProcessExit;
        }

        private void StartForm()
        {
            ApplicationConfiguration.Initialize();
            var shell = _serviceProvider.GetService<MainWindow>();
            Application.Run(shell);
        }

        private void OnProcessExit(object? sender, EventArgs e)
        {
            _applicationLifetime.StopApplication();
            UnregisterShutdownHandlers();
            applicationStartedRegistration.Dispose();
            applicationStoppingRegistration.Dispose();
        }

        private void UnregisterShutdownHandlers()
        {
            //AppDomain.CurrentDomain.ProcessExit -= OnProcessExit;
            Application.ApplicationExit -= OnProcessExit;
        }

        public void Dispose()
        {
        }
    }
}
