// Copyright (c) 2023 - 2026 Francesco Crimi
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using WpfApp2.Hosting;

namespace WpfApp2
{
    public partial class App : Application
    {
        private async void OnStartup(object sender, StartupEventArgs e)
        {
            await CreateHostBuilder(e.Args).Build().RunAsync();
        }

        private IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWPF<MainView>()
                .ConfigureServices(ConfigureServices);
        }

        private void ConfigureServices(HostBuilderContext hostBuilderContext,
                                       IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<MainView>();
        }
    }
}
