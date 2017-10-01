using GalaSoft.MvvmLight;
using Solitare.UI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitare.UI.Game.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public int Value { get; set; }

        public CardShape Shape { get; set; }

        public CardName Name { get; set; }

        public string Path { get; set; }
    }
}
