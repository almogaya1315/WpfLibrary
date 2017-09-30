using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Solitare.UI.Enums;
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
        readonly private RuleSetViewModel _ruleSetViewModel;

        public ICommand SetDraw;

        public OptionsViewModel(RuleSetViewModel ruleSetViewModel)
        {
            _ruleSetViewModel = ruleSetViewModel;

            SetDraw = new RelayCommand<InitialDraw>(SetInitialDraw);
        }

        private void SetInitialDraw(InitialDraw draw)
        {

        }
    }
}
