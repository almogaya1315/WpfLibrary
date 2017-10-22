using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Solitare.UI.Game.ViewModels;
using Solitare.UI.Options.ViewModels;
using System.Windows.Input;

namespace Solitare.UI.Menu.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        readonly private MainViewModel _mainViewModel;

        public ICommand NewGame { get; set; }
        public ICommand Options { get; set; }
        public ICommand Exit { get; set; }

        private RuleSetViewModel _ruleSet { get; set; }

        public MenuViewModel(MainViewModel mainViewModel, RuleSetViewModel ruleSet)
        {
            _mainViewModel = mainViewModel;
            _ruleSet = ruleSet;

            Exit = new RelayCommand(ExitGame);
            Options = new RelayCommand(NavigateToOptionsView);
            NewGame = new RelayCommand(StartNewGame);
        }

        private void ExitGame()
        {
            _mainViewModel.CloseFromMenu();
        }

        private void NavigateToOptionsView()
        {
            _mainViewModel.SwitchToOptionsView(new OptionsViewModel(_mainViewModel, this));
        }

        private void StartNewGame()
        {
            _mainViewModel.SwitchToGameView(new GameViewModel(_mainViewModel, this, _ruleSet));
        }
    }
}
