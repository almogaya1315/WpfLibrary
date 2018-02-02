using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TeamKits.Base;
using TeamKits.Report.ViewModels;
using TeamKits.Report.Views;

namespace TeamKits
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var windowManager = new WindowManager();

            var report = new ReportWindow(windowManager);
            report.DataContext = new ReportViewModel(windowManager);
            report.Show();
        }
    }
}
