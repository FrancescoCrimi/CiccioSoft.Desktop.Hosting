using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using System.Windows.Threading;
using WpfApp.Hosting;

namespace WpfApp
{
    public partial class App : Application
    {
        private async void OnStartup(object sender, StartupEventArgs e)
        {
            await CreateHostBuilder(e.Args).Build().RunAsync();
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
        }

        private IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWPF<MainWindow>()
            .ConfigureServices(ConfigureServices);

        private void ConfigureServices(HostBuilderContext hostBuilderContext,
                                       IServiceCollection serviceCollection) =>
            serviceCollection
                .AddTransient<MainWindow>();
    }
}
