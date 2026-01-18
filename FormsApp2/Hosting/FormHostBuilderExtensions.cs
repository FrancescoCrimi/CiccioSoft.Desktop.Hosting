// Copyright (c) 2023 - 2026 Francesco Crimi
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows.Forms;

namespace FormsApp2.Hosting
{
    public static class FormHostBuilderExtensions
    {
        public static IHostBuilder ConfigureWinForms<TForm>(this IHostBuilder hostBuilder) where TForm : Form
        {
            return hostBuilder
                .ConfigureServices((hostBuilderContext, serviceCollection) =>
                    serviceCollection
                        .AddSingleton<IHostLifetime, FormHostLifetime>()
                        .AddHostedService<FormHostedService<TForm>>());
        }
    }
}
