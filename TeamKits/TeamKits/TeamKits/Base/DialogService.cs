using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TeamKits.ViewModels;
using TeamKits.Views;

namespace TeamKits.Base
{
    public interface IDialogService
    {
        void ShowMessageBox(string message, string caption, string yesBtn = "OK", bool showNoBtn = false, string noBtn = "Cancel");
    }

    public class DialogService : IDialogService
    {
        public void ShowMessageBox(string message, string caption, string yesBtn = "OK", bool showNoBtn = false, string noBtn = "Cancel")
        {
            var dialogViewModel = new DialogViewModel(message, caption, yesBtn, showNoBtn, noBtn);

            var window = new DialogView()
            {
                DataContext = dialogViewModel,
                Topmost = true,
                Title = caption,
                ResizeMode = ResizeMode.NoResize,
                Height = 150,
                Width = 300,
            };

            window.ShowDialog();
        }
    }
}
