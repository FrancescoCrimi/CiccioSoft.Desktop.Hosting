// Copyright (c) 2023 - 2026 Francesco Crimi
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Microsoft.Extensions.Configuration;
using System.Windows;

namespace WpfApp2
{
    public partial class Window1 : Window
    {
        public Window1(IConfiguration config)
        {
            InitializeComponent();
            label.Content = config["CustomConf"] ?? "Default App Name";
        }
    }
}