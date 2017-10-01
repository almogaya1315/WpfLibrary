using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Solitare.UI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Windows.Input;

namespace Solitare.UI.Game.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private CardViewModel _backCard;
        private List<CardViewModel> _mainCards;
        private List<CardViewModel> _openCards;

        public ICommand Deal { get; set; }

        private CardViewModel _mainStackCard;
        public CardViewModel MainStackCard
        {
            get { return _mainStackCard; }
            set
            {
                _mainStackCard = value;
                RaisePropertyChanged();
            }
        }

        private CardViewModel _openStackCard;
        public CardViewModel OpenStackCard
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
            _backCard = new CardViewModel() { Path = Properties.Resources.BackCardPath };
            _openCards = new List<CardViewModel>();

            MainStackCard = _backCard;
            OpenStackCard = new CardViewModel() { Path = string.Empty };
            CreateDeck();

            Deal = new RelayCommand(DealCard);
        }

        private void DealCard()
        {
            if (MainStackCard.Path == Properties.Resources.BackCardPath)
            {
                SetMainStackCard();
                return;
            }
            else if (MainStackCard.Path != string.Empty)
            {
                OpenStackCard = MainStackCard;
                _mainCards.Remove(MainStackCard);
                _openCards.Add(OpenStackCard);
                SetMainStackCard();
                return;
            }
            else
            {
                OpenStackCard = new CardViewModel() { Path = string.Empty };
                _openCards.Where(op => op.Shape == CardShape.Hearts || op.Shape == CardShape.Clubs || 
                                       op.Shape == CardShape.Spades || op.Shape == CardShape.Diamonds).ToList().ForEach(op => _mainCards.Add(op));
                MainStackCard = _backCard;
            }
        }

        private void SetMainStackCard()
        {
            var randomShape = Enum.GetValues(typeof(CardShape)).Cast<CardShape>().First(e => (int)e == new Random().Next(1, 2));
            var randomName = Enum.GetValues(typeof(CardName)).Cast<CardName>().First(e => (int)e == new Random().Next(0, _mainCards.Count(c => c.Shape == randomShape)));

            while (!_mainCards.Exists(c => c.Shape == randomShape && c.Name == randomName))
            {
                randomShape = Enum.GetValues(typeof(CardShape)).Cast<CardShape>().First(e => (int)e == new Random().Next(1, 2));
                randomName = Enum.GetValues(typeof(CardName)).Cast<CardName>().First(e => (int)e == new Random().Next(0, _mainCards.Count(c => c.Shape == randomShape)));
            }
            
            MainStackCard = _mainCards.First(c => c.Shape == randomShape && c.Name == randomName);
        }

        private void CreateDeck()
        {
            _mainCards = new List<CardViewModel>();
            _mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Two, Value = 2, Path = "/Images/Spades/TwoOfSpades.jpg" });
            _mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Three, Value = 3, Path = "/Images/Spades/ThreeOfSpades.jpg" });
            _mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Four, Value = 4, Path = "/Images/Spades/FourOfSpades.jpg" });
            _mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Five, Value = 5, Path = "/Images/Spades/FiveOfSpades.jpg" });
            _mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Six, Value = 6, Path = "/Images/Spades/SixOfSpades.jpg" });
            _mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Seven, Value = 7, Path = "/Images/Spades/SevenOfSpades.jpg" });
            _mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Eight, Value = 8, Path = "/Images/Spades/EightOfSpades.jpg" });
            _mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Nine, Value = 9, Path = "/Images/Spades/NineOfSpades.jpg" });
            _mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Ten, Value = 10, Path = "/Images/Spades/TenOfSpades.jpg" });
            _mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Jack, Value = 11, Path = "/Images/Spades/JackOfSpades.jpg" });
            _mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Queen, Value = 12, Path = "/Images/Spades/QueenOfSpades.jpg" });
            _mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.King, Value = 13, Path = "/Images/Spades/KingOfSpades.jpg" });
            _mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Ace, Value = 14, Path = "/Images/Spades/AceOfSpades.jpg" });
        }
    }
}
