// Copyright (c) 2023 - 2026 Francesco Crimi
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using FormsApp2.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace FormsApp2
{
    internal static class Program
    {
        [STAThread]
        static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args);
            host.ConfigureWinForms<MainView>();
            host.ConfigureServices(ConfigureServices);
            return host;
        }

        private static void ConfigureServices(HostBuilderContext hostBuilderContext,
                                              IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<MainView>();
        }
    }
}