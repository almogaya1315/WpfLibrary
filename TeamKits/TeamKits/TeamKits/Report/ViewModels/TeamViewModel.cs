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
        private ReportViewModel _report;

        public int Id { get; set; }
        public string Name { get; set; }
        public TeamType Type { get; set; }

        private string _homeKitColor;
        public string HomeKitColor
        {
            get { return _homeKitColor; }
            set
            {
                if (_homeKitColor != null && !Equals(_homeKitColor, value)) _report.ChangeTeamColor(Id, value);
                _homeKitColor = value;
            }
        }
        public string AwayKitColor { get; set; }

        public List<PlayerViewModel> Squad { get; set; }

        public string TeamSymbolPicturePath { get; set; }
        public string TeamHomeKit1PicturePath { get; set; }
        public string TeamHomeKit2PicturePath { get; set; }
        public string TeamAwayKit1PicturePath { get; set; }
        public string TeamAwayKit2PicturePath { get; set; }

        public TeamViewModel(ReportViewModel report, string homeKitColor)
        {
            _report = report;

            HomeKitColor = homeKitColor;

            if (!_report.TeamColors.Exists(c => c == HomeKitColor)) _report.TeamColors.Add(HomeKitColor);
            else HomeKitColor = _report.TeamColors.Find(c => c == HomeKitColor);
        }
    }

    public enum TeamType
    {
        Home,
        Away,
    }
}
