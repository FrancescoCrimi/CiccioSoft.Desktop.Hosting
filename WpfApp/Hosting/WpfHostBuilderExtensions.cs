using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WpfApp.Hosting
{
    public static class WpfHostBuilderExtensions
    {
        public static IHostBuilder ConfigureWPF<TWindow>(this IHostBuilder hostBuilder) where TWindow : System.Windows.Window
        {
            hostBuilder
                .ConfigureServices((hostBuilderContext, serviceCollection) =>
                {
                    serviceCollection
                    .AddSingleton<IHostLifetime, WpfAppLifetime>()
                    .AddHostedService<WpfAppHostedService<TWindow>>();
                });
            return hostBuilder;
        }
    }
}
