using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace WpfApp.Hosting
{
    public static class WpfHostBuilderExtensions
    {
        public static IHostBuilder ConfigureWpfHost<MainWindoww>(this IHostBuilder hostBuilder) where MainWindoww : Window
        {
            return hostBuilder
                .ConfigureServices((hostBuilderContext, serviceCollection) => serviceCollection
                    .AddSingleton<IHostLifetime, WpfHostLifetime>()
                    .AddHostedService<WpfHostedService<MainWindoww>>());
        }
    }
}
