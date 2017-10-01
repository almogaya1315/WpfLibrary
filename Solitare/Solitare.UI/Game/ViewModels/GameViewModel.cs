using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Solitare.UI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Solitare.UI.Game.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private string _backCard = Properties.Resources.BackCardPath;
        private Dictionary<CardShape, Dictionary<CardName, CardViewModel>> _mainCards;
        private Dictionary<CardName, CardViewModel> _openCards;

        public ICommand Deal { get; set; }

        private string _mainStackCard;
        public string MainStackCard
        {
            get { return _mainStackCard; }
            set
            {
                _mainStackCard = value;
                RaisePropertyChanged();
            }
        }

        private string _openStackCard;
        public string OpenStackCard
        {
            get { return _openStackCard; }
            set
            {
                _openStackCard = value;
                RaisePropertyChanged();
            }
        }

        public GameViewModel()
        {
            MainStackCard = _backCard;
            OpenStackCard = string.Empty;
            CreateDeck();
            _openCards = new Dictionary<CardName, CardViewModel>();

            Deal = new RelayCommand(DealCard);
        }

        private void DealCard()
        {
            if (OpenStackCard == string.Empty)
            {
                if (MainStackCard == Properties.Resources.BackCardPath)
                {
                    var randomShape = Enum.GetValues(typeof(CardShape)).Cast<CardShape>().First(e => (int)e == new Random().Next(1, 2));
                    var randomName = Enum.GetValues(typeof(CardName)).Cast<CardName>().First(e => (int)e == new Random().Next(0, 4));
                    MainStackCard = _mainCards.First(cs => cs.Key == randomShape).Value.First(cn => cn.Key == randomName).Value.Path;
                }
                else
                {

                }
            }
        }

        private void CreateDeck()
        {
            _mainCards = new Dictionary<CardShape, Dictionary<CardName, CardViewModel>>();
            var spades = new Dictionary<CardName, CardViewModel>();
            spades.Add(CardName.Two,  new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Two, Value = 2, Path = "/Images/Spades/TwoOfSpades.jpg" });
            spades.Add(CardName.Three, "/Images/Spades/ThreeOfSpades.jpg");
            spades.Add(CardName.Four, "/Images/Spades/FourOfSpades.jpg");
            spades.Add(CardName.Five, "/Images/Spades/FiveOfSpades.jpg");
            spades.Add(CardName.Six, "/Images/Spades/SixOfSpades.jpg");
            spades.Add(CardName.Seven, "/Images/Spades/SevenOfSpades.jpg");
            spades.Add(CardName.Eight, "/Images/Spades/EightOfSpades.jpg");
            spades.Add(CardName.Nine, "/Images/Spades/NineOfSpades.jpg");
            spades.Add(CardName.Ten, "/Images/Spades/TenOfSpades.jpg");
            spades.Add(CardName.Jack, "/Images/Spades/JackOfSpades.jpg");
            spades.Add(CardName.Queen, "/Images/Spades/QueenOfSpades.jpg");
            spades.Add(CardName.King, "/Images/Spades/KingOfSpades.jpg");
            spades.Add(CardName.Ace, "/Images/Spades/AceOfSpades.jpg");
            _mainCards.Add(CardShape.Spades, spades);
        }
    }
}
