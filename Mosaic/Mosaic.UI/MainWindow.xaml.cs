using GalaSoft.MvvmLight;
using System.Windows;

namespace Mosaic.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }
    }
}
