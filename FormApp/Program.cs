// Copyright (c) 2023 - 2026 Francesco Crimi
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

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
