using GalaSoft.MvvmLight;
using Mosaic.UI.Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mosaic.UI
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase View { get; set; }

        public MainViewModel()
        {
            DataTemplate template = new DataTemplate();

            View = new MosaicViewModel();
        }
    }
}
