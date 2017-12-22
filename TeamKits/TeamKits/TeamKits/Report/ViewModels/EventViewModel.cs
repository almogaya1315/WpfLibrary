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
        public int TeamId { get; set; }
        public string TeamName { get; set; }

        private string _teamColor;
        public string TeamColor
        {
            get { return _teamColor; }
            set
            {
                _teamColor = value;
                RaisePropertyChanged();
            }
        }

        public PlayerViewModel Player1 { get; set; }
        public PlayerViewModel Player2 { get; set; }
        public PlayerViewModel Player3 { get; set; }
    }
}
