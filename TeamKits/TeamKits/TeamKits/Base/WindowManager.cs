using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TeamKits.Enums;
using TeamKits.Report.ViewModels;
using TeamKits.Report.Views;
using TeamKits.ViewModels;
using TeamKits.Views;

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

        public void ShowMessageBox(string message, string caption, string yesBtn = "OK", bool showNoBtn = false, string noBtn = "Cancel", BoxOperation operation = BoxOperation.Message, Action action = null)
        {
            var dialogViewModel = new DialogViewModel(message, caption, yesBtn, showNoBtn, noBtn, operation, action);

            var window = new DialogView()
            {
                DataContext = dialogViewModel,
                Topmost = true,
                Title = caption,
                ResizeMode = ResizeMode.NoResize,
                Height = 150,
                Width = 300,
                BorderBrush = Brushes.LightBlue,
            };

            window.ShowDialog();
        }
    }
}
