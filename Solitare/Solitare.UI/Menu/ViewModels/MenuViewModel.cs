using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Solitare.UI.Game.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Solitare.UI.Menu.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        readonly private MainViewModel _mainViewModel;

        public ICommand NewGame { get; set; }

        public ICommand Options { get; set; }

        public ICommand Exit { get; set; }

        public MenuViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            NewGame = new RelayCommand(StartNewGame);
            Exit = new RelayCommand(ExitGame);
        }

        private void StartNewGame()
        {
            _mainViewModel.SwitchToGameView(new GameViewModel());
        }

        private void ExitGame()
        {
            throw new NotImplementedException();
        }
    }
}
