using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamKits.Base;

namespace TeamKits.Report.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        private readonly WindowManager _windowManager;

        public GameViewModel Game { get; set; }

        public List<EventViewModel> Events { get; set; }
        public List<string> TeamColors { get; set; }

        public ICommand ShowTeamKits { get; set; }

        public ReportViewModel(WindowManager windowManager)
        {
            _windowManager = windowManager;

            SetTeamColors();
            SetGame();
            SetEvents();

            ShowTeamKits = new RelayCommand(ShowTeamKitsWindow);
        }

        private void SetTeamColors()
        {
            TeamColors = new List<string>()
            {
                "#ffff00", "#00b300", "#ff0000", "#0066ff", "#9900ff", "#e6e6e6", "#1a1a1a", "#ff9900",
            };
        }
        private void SetGame()
        {
            Game = new GameViewModel()
            {
                Id = 10001,
                Name = "Test",
                Season = "17/18",
                HomeTeam = new TeamViewModel(this, homeKitColor: "#4d4dff")
                {
                    Id = 100001,
                    Name = "Barcelona FC",
                    Type = TeamType.Home,
                    AwayKitColor = "#dfff80",

                    TeamSymbolPicturePath = "/Resources/Images/bfc_symbol.png",
                    TeamHomeKit1PicturePath = "/Resources/Images/bfc_homekit1.png",
                    TeamHomeKit2PicturePath = "/Resources/Images/bfc_homekit2.png",
                    TeamAwayKit1PicturePath = "/Resources/Images/bfc_awaykit1.png",
                    TeamAwayKit2PicturePath = "/Resources/Images/bfc_awaykit2.png",

                    Squad = new List<PlayerViewModel>()
                    {
                      new PlayerViewModel() { Id = 100 },
                      new PlayerViewModel() { Id = 101 },
                      new PlayerViewModel() { Id = 102 },
                      new PlayerViewModel() { Id = 103 },
                      new PlayerViewModel() { Id = 104 },
                    },
                },
                AwayTeam = new TeamViewModel(this, homeKitColor: "#e6e6e6")
                {
                    Id = 100002,
                    Name = "Real Madrid",
                    Type = TeamType.Away,
                    AwayKitColor = "#404040",

                    TeamSymbolPicturePath = "/Resources/Images/rm_symbol.png",
                    TeamHomeKit1PicturePath = "/Resources/Images/rm_homekit1.png",
                    TeamHomeKit2PicturePath = "/Resources/Images/rm_homekit2.png",
                    TeamAwayKit1PicturePath = "/Resources/Images/rm_awaykit1.png",
                    TeamAwayKit2PicturePath = "/Resources/Images/rm_awaykit2.png",

                    Squad = new List<PlayerViewModel>()
                    {
                        new PlayerViewModel() { Id = 105 },
                        new PlayerViewModel() { Id = 106 },
                        new PlayerViewModel() { Id = 107 },
                        new PlayerViewModel() { Id = 108 },
                        new PlayerViewModel() { Id = 109 },
                    },
                },
            };
        }
        private void SetEvents()
        {
            Events = new List<EventViewModel>()
            {
                new EventViewModel()
                {
                    Id = 1,
                    TeamId = Game.HomeTeam.Id,
                    TeamName = Game.HomeTeam.Name,
                    TeamColor = Game.HomeTeam.HomeKitColor,
                    Player1 = new PlayerViewModel() { Id = Game.HomeTeam.Squad[0].Id },
                    Player2 = new PlayerViewModel() { Id = Game.HomeTeam.Squad[4].Id },
                    Player3 = new PlayerViewModel() { Id = Game.HomeTeam.Squad[2].Id },
                },
                new EventViewModel()
                {
                    Id = 2,
                    TeamId = Game.HomeTeam.Id,
                    TeamName = Game.HomeTeam.Name,
                    TeamColor = Game.HomeTeam.HomeKitColor,
                    Player1 = new PlayerViewModel() { Id = Game.HomeTeam.Squad[3].Id },
                    Player2 = new PlayerViewModel() { Id = Game.HomeTeam.Squad[1].Id },
                    Player3 = new PlayerViewModel() { Id = Game.HomeTeam.Squad[0].Id },
                },
                new EventViewModel()
                {
                    Id = 3,
                    TeamId = Game.AwayTeam.Id,
                    TeamName = Game.AwayTeam.Name,
                    TeamColor = Game.HomeTeam.AwayKitColor,
                    Player1 = new PlayerViewModel() { Id = Game.AwayTeam.Squad[4].Id },
                    Player2 = new PlayerViewModel() { Id = Game.AwayTeam.Squad[0].Id },
                    Player3 = new PlayerViewModel() { Id = Game.AwayTeam.Squad[2].Id },
                },
                new EventViewModel()
                {
                    Id = 4,
                    TeamId = Game.HomeTeam.Id,
                    TeamName = Game.HomeTeam.Name,
                    TeamColor = Game.HomeTeam.HomeKitColor,
                    Player1 = new PlayerViewModel() { Id = Game.HomeTeam.Squad[4].Id },
                    Player2 = new PlayerViewModel() { Id = Game.HomeTeam.Squad[2].Id },
                    Player3 = new PlayerViewModel() { Id = Game.HomeTeam.Squad[3].Id },
                },
                new EventViewModel()
                {
                    Id = 5,
                    TeamId = Game.AwayTeam.Id,
                    TeamName = Game.AwayTeam.Name,
                    TeamColor = Game.HomeTeam.AwayKitColor,
                    Player1 = new PlayerViewModel() { Id = Game.AwayTeam.Squad[1].Id },
                    Player2 = new PlayerViewModel() { Id = Game.AwayTeam.Squad[0].Id },
                    Player3 = new PlayerViewModel() { Id = Game.AwayTeam.Squad[3].Id },
                },
            };
        }

        private void ShowTeamKitsWindow()
        {
            _windowManager.ShowTeamKitsWindow(this);
        }

        public void ChangeTeamColor(int teamId, string newColor)
        {
            Events.ForEach(e => e.TeamColor = e.TeamId == teamId ? newColor : e.TeamColor);
        }
    }
}
