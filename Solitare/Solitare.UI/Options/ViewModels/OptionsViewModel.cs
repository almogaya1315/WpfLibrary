using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Solitare.UI.Enums;
using Solitare.UI.Menu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Solitare.UI.Options.ViewModels
{
    public class OptionsViewModel : ViewModelBase
    {
        readonly private MainViewModel _mainViewModel;
        readonly private MenuViewModel _menuViewModel;
        readonly private RuleSetViewModel _ruleSetViewModel;

        private InitialDraw _draw;
        public bool IsOneCard { get; set; }
        public bool IsThreeCards { get; set; }

        public bool ScoreEnabled { get; set; }
        public bool TimerEnabled { get; set; }
        public bool CluesEnabled { get; set; }
        public bool UndoEnabled { get; set; }

        public ICommand BackToMenu { get; set; }
        public ICommand SetDraw { get; set; }
        public ICommand Save { get; set; }

        public OptionsViewModel(MainViewModel mainViewModel, MenuViewModel menuViewModel)
        {
            _mainViewModel = mainViewModel;
            _menuViewModel = menuViewModel;
            _ruleSetViewModel = _mainViewModel.RuleSetViewModel;

            SetInitialDraw(_ruleSetViewModel.Draw);
            ScoreEnabled = _ruleSetViewModel.ScoreEnabled;
            TimerEnabled = _ruleSetViewModel.TimerEnabled;
            CluesEnabled = _ruleSetViewModel.CluesEnabled;

            BackToMenu = new RelayCommand(BackToMainMenu);
            SetDraw = new RelayCommand<InitialDraw>(SetInitialDraw);
            Save = new RelayCommand(SaveRuleSet, CanSave);
        }

        private void BackToMainMenu()
        {
            _mainViewModel.SwitchToMenuView(_menuViewModel);
        }

        private void SetInitialDraw(InitialDraw draw)
        {
            _draw = draw;
            IsOneCard = _draw == InitialDraw.OneCard ? true : false;
            IsThreeCards = !IsOneCard;
        }

        private void SaveRuleSet()
        {
            if (_ruleSetViewModel.Draw != _draw) _ruleSetViewModel.Draw = _draw;
            if (_ruleSetViewModel.ScoreEnabled != ScoreEnabled) _ruleSetViewModel.ScoreEnabled = ScoreEnabled;
            if (_ruleSetViewModel.TimerEnabled != TimerEnabled) _ruleSetViewModel.TimerEnabled = TimerEnabled;
            if (_ruleSetViewModel.ScoreEnabled != ScoreEnabled) _ruleSetViewModel.ScoreEnabled = ScoreEnabled;
            if (_ruleSetViewModel.UndoEnabled != UndoEnabled) _ruleSetViewModel.UndoEnabled = UndoEnabled; 
        }

        private bool CanSave()
        {
            return ScoreEnabled != _ruleSetViewModel.ScoreEnabled || CluesEnabled != _ruleSetViewModel.CluesEnabled ||
                   TimerEnabled != _ruleSetViewModel.TimerEnabled || UndoEnabled != _ruleSetViewModel.UndoEnabled || 
                   _draw != _ruleSetViewModel.Draw;
        }
    }
}
