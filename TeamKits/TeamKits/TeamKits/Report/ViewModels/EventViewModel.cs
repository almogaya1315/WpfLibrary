using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamKits.Report.ViewModels
{
    public class EventViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string TeamColor { get; set; }
        public PlayerViewModel Player1 { get; set; }
        public PlayerViewModel Player2 { get; set; }
        public PlayerViewModel Player3 { get; set; }
    }
}
