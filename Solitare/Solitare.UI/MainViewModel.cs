using GalaSoft.MvvmLight;
using Solitare.UI.Game.ViewModels;
using Solitare.UI.Menu.ViewModels;
using Solitare.UI.Options.ViewModels;
using System;

namespace Solitare.UI
{
    public class MainViewModel : ViewModelBase
    {
        public event EventHandler CloseFromMenuEvent;

        private ViewModelBase _currentView;
        public ViewModelBase CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                RaisePropertyChanged();
            }
        }
        public RuleSetViewModel RuleSetViewModel;

        public MainViewModel()
        {
            RuleSetViewModel = new RuleSetViewModel();

            SwitchToMenuView(new MenuViewModel(this, RuleSetViewModel));
        }

        public void SwitchToMenuView(MenuViewModel menuViewModel)
        {
            CurrentView = menuViewModel;
        }

        public void SwitchToOptionsView(OptionsViewModel optionsViewModel)
        {
            CurrentView = optionsViewModel;
        }

        public void SwitchToGameView(GameViewModel gameViewModel)
        {
            CurrentView = gameViewModel;
        }

        public void CloseFromMenu()
        {
            CloseFromMenuEvent?.Invoke(this, null);
        }
    }
}
