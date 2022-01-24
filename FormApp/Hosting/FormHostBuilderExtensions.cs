using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows.Forms;

namespace FormApp.Hosting
{
    public static class FormHostBuilderExtensions
    {
        public static IHostBuilder ConfigureFormHost<MainWindoww>(this IHostBuilder hostBuilder) where MainWindoww : Form
        {
            return hostBuilder
                .ConfigureServices((hostBuilderContext, serviceCollection) => serviceCollection
                    .AddSingleton<IHostLifetime, FormHostLifetime>()
                    .AddHostedService<FormHostedService<MainWindoww>>());
        }
    }
}
