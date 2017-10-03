using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Solitare.UI.Enums;
using System.Collections.Generic;
using System.Linq;
using Solitare.UI.Extensions;
using System.Windows.Input;

namespace Solitare.UI.Game.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private CardViewModel _backCard;
        private CardViewModel _emptyCard;
        private List<CardViewModel> _mainCards;
        private List<CardViewModel> _openCards;

        private Dictionary<DeckName, List<CardViewModel>> _closedStacks;

        public ICommand Deal { get; set; }

        private CardViewModel _mainDeckCard;
        public CardViewModel MainDeckCard
        {
            get { return _mainDeckCard; }
            set
            {
                _mainDeckCard = value;
                RaisePropertyChanged();
            }
        }

        private CardViewModel _openDeckCard;
        public CardViewModel OpenDeckCard
        {
            get { return _openDeckCard; }
            set
            {
                _openDeckCard = value;
                RaisePropertyChanged();
            }
        }

        private CardViewModel _diamondsDeckCard;
        public CardViewModel DiamondsDeckCard
        {
            get { return _diamondsDeckCard; }
            set
            {
                _diamondsDeckCard = value;
                RaisePropertyChanged();
            }
        }

        public GameViewModel()
        {
            _emptyCard = new CardViewModel() { Path = Properties.Resources.EmptyCardPath };
            _backCard = new CardViewModel() { Path = Properties.Resources.BackCardPath };
            _openCards = new List<CardViewModel>();

            MainDeckCard = _backCard;
            OpenDeckCard = new CardViewModel() { Path = string.Empty };
            CreateDeck();

            Deal = new RelayCommand(DealCard);
        }

        public void MoveCard(string sourceDeck, string targetDeck, string cardName, string cardShape)
        {
            

            // TODO..
        }

        private void DealCard()
        {
            if (MainDeckCard.Path == Properties.Resources.BackCardPath)
            {
                MainDeckCard = _mainCards.Last();
                return;
            }
            else if (MainDeckCard.Path != Properties.Resources.EmptyCardPath)
            {
                OpenDeckCard = MainDeckCard;
                _mainCards.Remove(MainDeckCard);
                _openCards.Add(OpenDeckCard);
                MainDeckCard = _mainCards.LastOrDefault() ?? _emptyCard;
                return;
            }
            else
            {
                OpenDeckCard = new CardViewModel() { Path = string.Empty };
                _openCards.ForEach(op => _mainCards.Add(op));
                _openCards.Clear();
                MainDeckCard = _backCard;
            }
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

            _mainCards.Shuffle();
        }
    }
}
