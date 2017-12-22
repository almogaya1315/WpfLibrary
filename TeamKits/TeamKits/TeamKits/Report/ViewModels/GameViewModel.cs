using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamKits.Report.ViewModels;

namespace TeamKits.Report.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TeamViewModel HomeTeam { get; set; }
        public TeamViewModel AwayTeam { get; set; }
    }
}
