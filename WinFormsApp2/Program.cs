// Copyright (c) 2023 - 2026 Francesco Crimi
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using WinFormsApp2.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    internal static class Program
    {
        [STAThread]
        static async Task Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.ConfigureWinForms<Form1>();
            builder.Services
                .AddSingleton<Form1>();
            await builder.Build().RunAsync();
        }
    }
}