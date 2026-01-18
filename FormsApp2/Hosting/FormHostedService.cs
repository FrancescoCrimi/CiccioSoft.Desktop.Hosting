// Copyright (c) 2023 - 2026 Francesco Crimi
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsApp2.Hosting
{
    public class FormHostedService<TForm> : IHostedService, IDisposable where TForm : Form
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public FormHostedService(ILogger<FormHostedService<TForm>> logger,
                                 IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _logger.LogDebug("Created: {HashCode}", GetHashCode().ToString());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.CompletedTask;
            }

            var form = _serviceProvider.GetRequiredService<TForm>();
            Application.Run(form);

            _logger.LogDebug("StartAsync: {HashCode}", GetHashCode().ToString());
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Stopping WinForms application.");
            //foreach (var form in System.Windows.Forms.Application.OpenForms.Cast<Form>().ToList())
            //{
            //    try
            //    {
            //        form.Close();
            //        form.Dispose();
            //    }
            //    catch (Exception ex)
            //    {
            //        _logger.LogWarning(ex, "Couldn't cleanup a Form");
            //    }
            //}
            //System.Windows.Forms.Application.ExitThread();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposed: {HashCode}", GetHashCode().ToString());
        }
    }
}
