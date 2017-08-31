using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.ViewModels
{
    public class BallViewModel : ViewModelBase
    {
        public int Inning { get; set; }

        public int Over { get; set; }

        public int BallNumber { get; set; }

        public int IllegalBalls { get; set; }

        public int BowlerId { get; set; }

        public int BatsmanId { get; set; }

        public int NonStrikerId { get; set; }

        public int Runs { get; set; }

        public int RunType1 { get; set; }

        public int RunType2 { get; set; }

        public int RunType3 { get; set; }

        public bool Wicket { get; set; }

        public int WicketTypeId { get; set; }
    }
}
