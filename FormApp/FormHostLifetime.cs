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
        private readonly ILogger logger;
        private readonly IHostEnvironment environment;
        private readonly IHostApplicationLifetime applicationLifetime;
        private readonly IServiceProvider serviceProvider;
        private CancellationTokenRegistration applicationStartedRegistration;
        private CancellationTokenRegistration applicationStoppingRegistration;

        public FormHostLifetime(ILoggerFactory loggerFactory,
                                IHostEnvironment environment,
                                IHostApplicationLifetime applicationLifetime,
                                IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.environment = environment;
            this.applicationLifetime = applicationLifetime;
            logger = loggerFactory.CreateLogger("Microsoft.Hosting.Lifetime");
        }

        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            applicationStartedRegistration = applicationLifetime.ApplicationStarted.Register(state =>
            {
                ((FormHostLifetime)state!).OnApplicationStarted();
            },
            this);
            applicationStoppingRegistration = applicationLifetime.ApplicationStopping.Register(state =>
            {
                ((FormHostLifetime)state!).OnApplicationStopping();
            },
            this);

            RegisterShutdownHandlers();

            var threadfrm = new System.Threading.Thread(StartForm);
            threadfrm.SetApartmentState(System.Threading.ApartmentState.STA);
            threadfrm.Name = "StartForm Thread";
            threadfrm.Start();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
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

        private void RegisterShutdownHandlers()
        {
            //AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            Application.ApplicationExit += OnProcessExit;
        }

        private void StartForm()
        {
            ApplicationConfiguration.Initialize();
            var shell = serviceProvider.GetService<MainWindow>();
            Application.Run(shell);
        }

        private void OnProcessExit(object? sender, EventArgs e)
        {
            applicationLifetime.StopApplication();
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
