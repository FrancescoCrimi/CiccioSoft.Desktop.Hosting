// Copyright (c) 2023 - 2026 Francesco Crimi
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    internal static class Program
    {
        [STAThread]
        static async Task Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services
                .AddSingleton<IHostLifetime, FormHostLifetime>()
                .AddSingleton<Form1>();

            IHost host = builder.Build();
            await host.StartAsync();

            ApplicationConfiguration.Initialize();
            var form = host.Services.GetRequiredService<Form1>();
            Application.Run(form);
        }
    }
}