using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TeamKits.ViewModels
{
    public class DialogViewModel : ViewModelBase
    {
        public string Message { get; set; }
        public string Caption { get; set; }
        public string Yes { get; set; }
        public string No { get; set; }
        public bool ShowNoBtn { get; set; }

        public ICommand YesBtn { get; set; }
        public ICommand NoBtn { get; set; }

        public DialogViewModel(string message, string caption, string yesBtn, bool showNoBtn, string noBtn)
        {
            Message = message;
            Caption = caption;
            Yes = yesBtn;
            No = noBtn;
            ShowNoBtn = showNoBtn;

            YesBtn = new RelayCommand(YesButtonPressed);
            NoBtn = new RelayCommand(NoButtonPressed);
        }

        private void NoButtonPressed()
        {
            //Application.Current.Windows.Cast<>
        }

        private void YesButtonPressed()
        {
            Application.Current.Shutdown();
        }
    }
}
