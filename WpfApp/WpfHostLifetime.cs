using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp{
    public class WpfHostLifetime : IHostLifetime, IDisposable
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IHostEnvironment environment;
        private readonly IHostApplicationLifetime applicationLifetime;
        private readonly ILogger logger;
        private CancellationTokenRegistration _applicationStartedRegistration;
        private CancellationTokenRegistration _applicationStoppingRegistration;

        public WpfHostLifetime(IServiceProvider serviceProvider,
                               IHostEnvironment environment,
                               IHostApplicationLifetime applicationLifetime,
                               ILoggerFactory loggerFactory)
        {
            this.serviceProvider = serviceProvider;
            this.environment = environment;
            this.applicationLifetime = applicationLifetime;
            logger = loggerFactory.CreateLogger("Microsoft.Hosting.Lifetime");
        }

        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            _applicationStartedRegistration = applicationLifetime.ApplicationStarted.Register(state =>
            {
                ((WpfHostLifetime)state!).OnApplicationStarted();
            },
            this);
            _applicationStoppingRegistration = applicationLifetime.ApplicationStopping.Register(state =>
            {
                ((WpfHostLifetime)state!).OnApplicationStopping();
            },
            this);

            RegisterShutdownHandlers();

            var shell = serviceProvider.GetRequiredService<MainWindow>();
            shell.Show();

            return Task.CompletedTask;
        }

        private void RegisterShutdownHandlers()
        {
            //App.Current.Exit += OnProcessExit;
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
        }

        private void OnApplicationStarted()
        {
            logger.LogInformation("Application started.");
            logger.LogInformation("Hosting environment: {envName}", environment.EnvironmentName);
            logger.LogInformation("Content root path: {contentRoot}", environment.ContentRootPath);
        }

        private void OnApplicationStopping()
        {
            logger.LogInformation("Application is shutting down...");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void UnregisterShutdownHandlers()
        {
            //App.Current.Exit -= OnProcessExit;
            AppDomain.CurrentDomain.ProcessExit -= OnProcessExit;
        }

        private void OnProcessExit(object? sender, EventArgs e)
        {
            applicationLifetime.StopApplication();
            UnregisterShutdownHandlers();
            _applicationStartedRegistration.Dispose();
            _applicationStoppingRegistration.Dispose();
        }

        public void Dispose()
        {
        }
    }
}
