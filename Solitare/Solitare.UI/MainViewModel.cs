using GalaSoft.MvvmLight;
using Solitare.UI.Menu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitare.UI
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentView { get; set; }

        public MainViewModel()
        {
            CurrentView = new MenuViewModel();
        }
    }
}
