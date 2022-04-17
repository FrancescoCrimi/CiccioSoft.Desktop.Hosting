using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace FormApp
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).StartAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(serviceCollection =>
                {
                    serviceCollection
                    .AddSingleton<IHostLifetime, FormHostLifetime>()
                    .AddSingleton<MainWindow>();
                });
        }
    }
}
