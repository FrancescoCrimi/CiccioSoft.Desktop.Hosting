// Copyright (c) 2023 - 2026 Francesco Crimi
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace WpfApp2.Hosting
{
    public static class WpfHostBuilderExtensions
    {
        public static IHostBuilder ConfigureWPF<TWindow>(this IHostBuilder hostBuilder) where TWindow : Window
        {
            hostBuilder.ConfigureServices((hostBuilderContext, serviceCollection) => serviceCollection
                .AddSingleton<IHostLifetime, WpfAppLifetime>()
                .AddHostedService<WpfAppHostedService<TWindow>>());
            return hostBuilder;
        }
    }
}
