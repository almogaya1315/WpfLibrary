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

        private GameViewModel _game;

        public List<EventViewModel> Events { get; set; }

        public ICommand ShowTeamKits { get; set; }

        public ReportViewModel(WindowManager windowManager)
        {
            _windowManager = windowManager;

            SetGame();
            SetEvents();

            ShowTeamKits = new RelayCommand(ShowTeamKitsWindow);
        }

        private void SetGame()
        {
            _game = new GameViewModel()
            {
                Id = 10001,
                Name = "Test",
                HomeTeam = new TeamViewModel()
                {
                    Id = 100001,
                    Name = "Barcelona FC",
                    HomeKitColor = "#4d4dff",
                    AwayKitColor = "#dfff80",
                    Squad = new List<PlayerViewModel>()
                    {
                      new PlayerViewModel() { Id = 100 },
                      new PlayerViewModel() { Id = 101 },
                      new PlayerViewModel() { Id = 102 },
                      new PlayerViewModel() { Id = 103 },
                      new PlayerViewModel() { Id = 104 },
                    },
                },
                AwayTeam = new TeamViewModel()
                {
                    Id = 100002,
                    Name = "Real Madrid",
                    HomeKitColor = "#e6e6e6",
                    AwayKitColor = "#404040",
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
                    TeamColor = _game.HomeTeam.HomeKitColor,
                    Player1 = new PlayerViewModel() { Id = _game.HomeTeam.Squad[0].Id },
                    Player2 = new PlayerViewModel() { Id = _game.HomeTeam.Squad[4].Id },
                    Player3 = new PlayerViewModel() { Id = _game.HomeTeam.Squad[2].Id },
                },
                new EventViewModel()
                {
                    Id = 2,
                    TeamColor = _game.HomeTeam.HomeKitColor,
                    Player1 = new PlayerViewModel() { Id = _game.HomeTeam.Squad[3].Id },
                    Player2 = new PlayerViewModel() { Id = _game.HomeTeam.Squad[1].Id },
                    Player3 = new PlayerViewModel() { Id = _game.HomeTeam.Squad[0].Id },
                },
                new EventViewModel()
                {
                    Id = 3,
                    TeamColor = _game.HomeTeam.AwayKitColor,
                    Player1 = new PlayerViewModel() { Id = _game.AwayTeam.Squad[4].Id },
                    Player2 = new PlayerViewModel() { Id = _game.AwayTeam.Squad[0].Id },
                    Player3 = new PlayerViewModel() { Id = _game.AwayTeam.Squad[2].Id },
                },
                new EventViewModel()
                {
                    Id = 4,
                    TeamColor = _game.HomeTeam.HomeKitColor,
                    Player1 = new PlayerViewModel() { Id = _game.HomeTeam.Squad[4].Id },
                    Player2 = new PlayerViewModel() { Id = _game.HomeTeam.Squad[2].Id },
                    Player3 = new PlayerViewModel() { Id = _game.HomeTeam.Squad[3].Id },
                },
                new EventViewModel()
                {
                    Id = 5,
                    TeamColor = _game.HomeTeam.AwayKitColor,
                    Player1 = new PlayerViewModel() { Id = _game.AwayTeam.Squad[1].Id },
                    Player2 = new PlayerViewModel() { Id = _game.AwayTeam.Squad[0].Id },
                    Player3 = new PlayerViewModel() { Id = _game.AwayTeam.Squad[3].Id },
                },
            };
        }

        private void ShowTeamKitsWindow()
        {
            _windowManager.ShowTeamKitsWindow(this);
        }
    }
}
