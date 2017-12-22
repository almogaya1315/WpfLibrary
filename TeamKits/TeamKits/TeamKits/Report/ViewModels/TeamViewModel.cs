using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamKits.Report.ViewModels
{
    public class TeamViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HomeKitColor { get; set; }
        public string AwayKitColor { get; set; }
        public List<PlayerViewModel> Squad { get; set; }
    }
}
