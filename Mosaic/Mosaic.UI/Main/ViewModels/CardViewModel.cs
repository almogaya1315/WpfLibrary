using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosaic.UI.Main.ViewModels
{
    public class CardViewModel
    {
        public int Value { get; set; }

        public string TemplateName { get; set; }

        public string DataContextPath { get; set; }

        public CardType Type { get; set; }
    }

    public enum CardType
    {
        EmptyCard,
        UpCard,
        DownCard,
        LeftCard,
        RightCard,
        None,
    }
}
