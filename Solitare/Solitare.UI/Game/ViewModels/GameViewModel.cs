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
        private CardViewModel _transparentCard;
        private CardViewModel _moveableCard;
        private List<CardViewModel> _mainCards;
        private List<CardViewModel> _openCards;

        private Dictionary<DeckName, List<CardViewModel>> _closedDecks;

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
            // _emptyCard
            _transparentCard = new CardViewModel() { Path = string.Empty }; // new CardViewModel() { Path = Properties.Resources.EmptyCardPath };
            _backCard = new CardViewModel() { Path = Properties.Resources.BackCardPath };
            _openCards = new List<CardViewModel>();

            MainDeckCard = _backCard;
            OpenDeckCard = _transparentCard;

            CreateMainDeck();
            CreateClosedDecks();

            Deal = new RelayCommand(DealCard);
        }

        public void SetMoveableCardBinding(CardName cardName, CardShape cardShape, DeckName sourceDeck)
        {
            switch (sourceDeck)
            {
                case DeckName.OpenDeckCard:
                    _moveableCard = new CardViewModel() { Name = cardName, Shape = cardShape };
                    OpenDeckCard = new CardViewModel(_transparentCard);
                    break;
                case DeckName.DiamondsDeckCard:
                    break;
                default:
                    break;
            }
        }

        public void DropCard(DeckName sourceDeck, DeckName targetDeck, CardName cardName, CardShape cardShape, string path)
        {
            if (_closedDecks.ContainsKey(targetDeck))
            {
                _closedDecks[targetDeck].Add(new CardViewModel() { Name = cardName, Shape = cardShape, Path = path });

                switch (targetDeck)
                {
                    case DeckName.OpenDeckCard:
                        break;
                    case DeckName.DiamondsDeckCard:
                        DiamondsDeckCard = new CardViewModel(_closedDecks[targetDeck].Last());
                        break;
                    default:
                        break;
                }
            }

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
                MainDeckCard = _mainCards.LastOrDefault() ?? _transparentCard;
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

        private void CreateMainDeck()
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

        private void CreateClosedDecks()
        {
            _closedDecks = new Dictionary<DeckName, List<CardViewModel>>();
            _closedDecks[DeckName.OpenDeckCard] = new List<CardViewModel>();
            _closedDecks[DeckName.DiamondsDeckCard] = new List<CardViewModel>();
        }
    }
}
