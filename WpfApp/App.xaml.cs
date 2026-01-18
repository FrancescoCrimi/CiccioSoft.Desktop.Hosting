// Copyright (c) 2023 - 2026 Francesco Crimi
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

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
