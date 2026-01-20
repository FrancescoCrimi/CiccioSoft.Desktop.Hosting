// Copyright (c) 2023 - 2026 Francesco Crimi
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace WpfApp1
{
    public partial class App : Application
    {
        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(e.Args);
            builder.Services
                .AddSingleton<IHostLifetime, WpfHostLifetime>()
                .AddTransient<Window1>();

            IHost host = builder.Build();
            await host.StartAsync();

            var shell = host.Services.GetRequiredService<Window1>();
            shell.Show();
        }
    }
}
