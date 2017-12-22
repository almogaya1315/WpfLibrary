using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TeamKits.Report.ViewModels;
using TeamKits.Report.Views;

namespace TeamKits.Base
{
    public class WindowManager
    {
        public void ShowTeamKitsWindow(ReportViewModel report)
        {
            var window = new TeamKitsWindow()
            {
                Title = "Team Kits",
                DataContext = report,
                Topmost = true,
                Height = 800,
                Width = 1200,
                BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#41b1e1"),
                BorderThickness = new Thickness(1.0),
            };

            window.Show();
        }
    }
}
