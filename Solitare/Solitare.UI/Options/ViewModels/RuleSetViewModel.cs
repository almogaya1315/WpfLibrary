using GalaSoft.MvvmLight;
using Solitare.UI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitare.UI.Options.ViewModels
{
    public class RuleSetViewModel : ViewModelBase
    {
        public RuleSetViewModel()
        {
            Draw = InitialDraw.OneCard;
            ScoreEnabled = TimerEnabled = CluesEnabled = true;
        }

        public InitialDraw Draw { get; set; }

        public bool ScoreEnabled { get; set; }

        public bool TimerEnabled { get; set; }

        public bool CluesEnabled { get; set; }

        // TODO..
        public bool UndoOnMove { get; set; }
    }
}
