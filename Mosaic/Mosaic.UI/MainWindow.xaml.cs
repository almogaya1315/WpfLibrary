using GalaSoft.MvvmLight;
using Mosaic.UI.Main.ViewModels;
using System.Windows;

namespace Mosaic.UI
{
    public partial class MainWindow : Window
    {
        public ViewModelBase View { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            View = new MosaicViewModel();
        }
    }
}
