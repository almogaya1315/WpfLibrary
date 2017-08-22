using GalaSoft.MvvmLight;
using Main.ViewModel;
using Main.Views;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Main
{
    public class MainViewModel : ViewModelBase
    {
        public IControlViewModel Control { get; set; }

        public MainViewModel()
        {
            Control = new DataViewModel();
        }
    }
}