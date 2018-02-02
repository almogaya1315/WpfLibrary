using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TeamKits.Base;

namespace TeamKits.Report.Views
{
    public partial class ReportWindow : MetroWindow
    {
        readonly private WindowManager _windowManager;

        public ReportWindow(WindowManager windowManager)
        {
            InitializeComponent();

            _windowManager = windowManager;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _windowManager.ShowMessageBox($"Are you sure?", "Exit", showNoBtn: true, operation: Enums.BoxOperation.Exit);

            e.Cancel = true;
            base.OnClosing(e);
        }
    }
}
