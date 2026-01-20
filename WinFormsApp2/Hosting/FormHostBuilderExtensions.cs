// Copyright (c) 2023 - 2026 Francesco Crimi
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows.Forms;

namespace WinFormsApp2.Hosting
{
    public static class FormHostBuilderExtensions
    {
        public static HostApplicationBuilder ConfigureWinForms<TForm>(this HostApplicationBuilder builder) where TForm : Form
        {
            builder.Services
                .AddSingleton<IHostLifetime, FormHostLifetime>()
                .AddHostedService<FormHostedService<TForm>>();
            return builder;
        }
    }
}
