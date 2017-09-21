using GalaSoft.MvvmLight;
using System.Windows;
using System.Windows.Controls;

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
