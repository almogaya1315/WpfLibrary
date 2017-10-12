using GalaSoft.MvvmLight;
using Solitare.UI.Controls.Canvas;
using Solitare.UI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Solitare.UI.Game.ViewModels
{
    public class ContainerViewModel : ViewModelBase
    {
        public string CardPath { get; set; }

        public string FrontCardPath { get; set; }

        public CardName CardName { get; set; }

        public CardShape CardShape { get; set; }

        public int CardValue { get; set; }

        public ContainerViewModel SubContainer { get; set; }

        public EventResource TakeCardEventResource { get; set; }
    }
}
