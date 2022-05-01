using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace WpfApp
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await CreateHostBuilder(e.Args).StartAsync();
        }

        private IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(serviceCollection =>
                {
                    serviceCollection
                    .AddSingleton<IHostLifetime, WpfHostLifetime>()
                    .AddTransient<MainWindow>();
                });
        }
    }
}
