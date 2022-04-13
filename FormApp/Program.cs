using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace FormApp
{
    public static class Program
    {
        [STAThread]
        public static async Task Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
            await CreateHostBuilder(args).Build().RunAsync();
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
