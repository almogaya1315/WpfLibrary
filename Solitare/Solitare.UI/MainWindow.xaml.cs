using System.ComponentModel;
using System.Windows;

namespace Solitare.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var main = DataContext = new MainViewModel();
            (main as MainViewModel).CloseFromMenuEvent += (s, e) => Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var result = MessageBox.Show(Properties.Resources.ExitComfirmation, Properties.Resources.ExitCaption, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK) e.Cancel = false;
            else e.Cancel = true;

            base.OnClosing(e);
        }
    }
}
