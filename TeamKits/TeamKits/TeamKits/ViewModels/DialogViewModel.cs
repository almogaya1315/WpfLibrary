using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TeamKits.Enums;

namespace TeamKits.ViewModels
{
    public class DialogViewModel : ViewModelBase
    {
        private BoxOperation _operation;
        private Action _action;

        public string Message { get; set; }
        public string Caption { get; set; }
        public string Yes { get; set; }
        public string No { get; set; }
        public bool ShowNoBtn { get; set; }

        public ICommand YesBtn { get; set; }
        public ICommand NoBtn { get; set; }

        public DialogViewModel(string message, string caption, string yesBtn, bool showNoBtn, string noBtn, BoxOperation operation, Action action)
        {
            _operation = operation;
            _action = action;

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
            CloseMessageBox();
        }

        private void YesButtonPressed()
        {
            switch (_operation)
            {
                case BoxOperation.Exit:
                    Application.Current.Shutdown();
                    break;
                case BoxOperation.UserDone:
                    _action?.Invoke();
                    break;
            }

            CloseMessageBox();
        }

        private void CloseMessageBox()
        {
            Application.Current.Windows.Cast<Window>().Single(w => w.DataContext == this).Close();
        }
    }
}
