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
            var kitsWindow = new MetroWindow()
            {
                Title = "Team Kits",
                DataContext = report,
                Content = new TeamKitsView(),
                Topmost = true,
                BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#41b1e1"),
                BorderThickness = new Thickness(1.0),
            };

            kitsWindow.Show();
        }
    }
}
