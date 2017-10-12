﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Solitare.UI.Enums;
using System.Collections.Generic;
using System.Linq;
using Solitare.UI.Extensions;
using System.Windows.Input;
using System;
using System.Windows;
using Solitare.UI.Controls.Image;
using Solitare.UI.Controls.Canvas;

namespace Solitare.UI.Game.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private CardViewModel _backCard;
        private CardViewModel _transparentCard;
        private ContainerViewModel _transparentContainer;
        private CardViewModel _moveableCard;

        private Dictionary<DeckName, List<CardViewModel>> _closedDecks;
        private Dictionary<DeckName, List<ContainerViewModel>> _openDecks;

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
        private CardViewModel _heartsDeckCard;
        public CardViewModel HeartsDeckCard
        {
            get { return _heartsDeckCard; }
            set
            {
                _heartsDeckCard = value;
                RaisePropertyChanged();
            }
        }
        private CardViewModel _spadesDeckCard;
        public CardViewModel SpadesDeckCard
        {
            get { return _spadesDeckCard; }
            set
            {
                _spadesDeckCard = value;
                RaisePropertyChanged();
            }
        }
        private CardViewModel _clubsDeckCard;
        public CardViewModel ClubsDeckCard
        {
            get { return _clubsDeckCard; }
            set
            {
                _clubsDeckCard = value;
                RaisePropertyChanged();
            }
        }

        private List<ContainerViewModel> _firstDeckCards;
        public List<ContainerViewModel> FirstDeckCards
        {
            get { return _firstDeckCards; }
            set
            {
                _firstDeckCards = value;
                RaisePropertyChanged();
            }
        }
        private List<ContainerViewModel> _secondDeckCards;
        public List<ContainerViewModel> SecondDeckCards
        {
            get { return _secondDeckCards; }
            set
            {
                _secondDeckCards = value;
                RaisePropertyChanged();
            }
        }
        private List<ContainerViewModel> _thirdDeckCards;
        public List<ContainerViewModel> ThirdDeckCards
        {
            get { return _thirdDeckCards; }
            set
            {
                _thirdDeckCards = value;
                RaisePropertyChanged();
            }
        }
        private List<ContainerViewModel> _fourthDeckCards;
        public List<ContainerViewModel> FourthDeckCards
        {
            get { return _fourthDeckCards; }
            set
            {
                _fourthDeckCards = value;
                RaisePropertyChanged();
            }
        }
        private List<ContainerViewModel> _fifthDeckCards;
        public List<ContainerViewModel> FifthDeckCards
        {
            get { return _fifthDeckCards; }
            set
            {
                _fifthDeckCards = value;
                RaisePropertyChanged();
            }
        }
        private List<ContainerViewModel> _sixthDeckCards;
        public List<ContainerViewModel> SixthDeckCards
        {
            get { return _sixthDeckCards; }
            set
            {
                _sixthDeckCards = value;
                RaisePropertyChanged();
            }
        }
        private List<ContainerViewModel> _seventhDeckCards;
        public List<ContainerViewModel> SeventhDeckCards
        {
            get { return _seventhDeckCards; }
            set
            {
                _seventhDeckCards = value;
                RaisePropertyChanged();
            }
        }

        public EventResource TakeCardEventResource { get; set; }

        public GameViewModel()
        {
            _transparentCard = new CardViewModel() { Path = Properties.Resources.EmptyCardPath};
            _transparentContainer = new ContainerViewModel() { CardPath = _transparentCard.Path };
            _backCard = new CardViewModel() { Path = Properties.Resources.BackCardPath };

            TakeCardEventResource = new EventResource(EventName.MouseLeftButtonDownEvent);

            CreateClosedDecks();
            CreateOpenDecks();

            Deal = new RelayCommand(DealCard);
        }

        public void SetMoveableCardBinding(CardName cardName, CardShape cardShape, int cardValue, DeckName sourceDeck, string path)
        {
            _moveableCard = new CardViewModel() { Name = cardName, Shape = cardShape, Value = cardValue, Path = path };

            if (_closedDecks.ContainsKey(sourceDeck))
            {
                _closedDecks[sourceDeck].RemoveAll(c => c.Name == cardName && c.Shape == cardShape);
                var cardBehind = _closedDecks[sourceDeck].LastOrDefault() ?? _transparentCard;

                switch (sourceDeck)
                {
                    case DeckName.OpenDeckCard:
                        OpenDeckCard = cardBehind;
                        break;
                    case DeckName.DiamondsDeckCard:
                        DiamondsDeckCard = cardBehind;
                        break;
                    case DeckName.HeartsDeckCard:
                        HeartsDeckCard = cardBehind;
                        break;
                    case DeckName.SpadesDeckCard:
                        SpadesDeckCard = cardBehind;
                        break;
                    case DeckName.ClubsDeckCard:
                        ClubsDeckCard = cardBehind;
                        break;
                }
            }
            else
            {
                _openDecks[sourceDeck].RemoveAll(c => c.CardName == cardName && c.CardShape == cardShape);
                _openDecks[sourceDeck].Last().SubContainer = null;

                switch (sourceDeck)
                {
                    case DeckName.FirstDeck:
                        FirstDeckCards = new List<ContainerViewModel>(_openDecks[sourceDeck]);
                        break;
                    case DeckName.SecondDeck:
                        SecondDeckCards = new List<ContainerViewModel>(_openDecks[sourceDeck]);
                        break;
                    case DeckName.ThirdDeck:
                        ThirdDeckCards = new List<ContainerViewModel>(_openDecks[sourceDeck]);
                        break;
                    case DeckName.FourthDeck:
                        FourthDeckCards = new List<ContainerViewModel>(_openDecks[sourceDeck]);
                        break;
                    case DeckName.FifthDeck:
                        FifthDeckCards = new List<ContainerViewModel>(_openDecks[sourceDeck]);
                        break;
                    case DeckName.SixthDeck:
                        SixthDeckCards = new List<ContainerViewModel>(_openDecks[sourceDeck]);
                        break;
                    case DeckName.SeventhDeck:
                        SeventhDeckCards = new List<ContainerViewModel>(_openDecks[sourceDeck]);
                        break;
                }
            }
        }

        public void SetFllipedCardBinding(DeckName sourceDeck, CardName cardName, CardShape cardShape)
        {
            var containerToFlip = _openDecks[sourceDeck].Find(c => c.CardName == cardName && c.CardShape == cardShape);

            if (containerToFlip.SubContainer != null) return;

            var frontPath = containerToFlip.FrontCardPath;
            containerToFlip.CardPath = frontPath;

            switch (sourceDeck)
            {
                case DeckName.FirstDeck:
                    FirstDeckCards = _openDecks[sourceDeck];
                    break;
                case DeckName.SecondDeck:
                    SecondDeckCards = _openDecks[sourceDeck];
                    break;
                case DeckName.ThirdDeck:
                    ThirdDeckCards = _openDecks[sourceDeck];
                    break;
                case DeckName.FourthDeck:
                    FourthDeckCards = _openDecks[sourceDeck];
                    break;
                case DeckName.FifthDeck:
                    FifthDeckCards = _openDecks[sourceDeck];
                    break;
                case DeckName.SixthDeck:
                    SixthDeckCards = _openDecks[sourceDeck];
                    break;
                case DeckName.SeventhDeck:
                    SeventhDeckCards = _openDecks[sourceDeck];
                    break;
                default:
                    break;
            }
        }

        public DeckMatch ValidateDeck(string deckName)
        {
            DeckMatch matchState = DeckMatch.NotFound;

            var targetDeck = Enum.GetValues(typeof(DeckName)).Cast<DeckName>().First(e => e.ToString() == deckName);
            if (_closedDecks.ContainsKey(targetDeck))
            {
                var targetCard = _closedDecks[targetDeck].LastOrDefault();

                if (targetDeck == DeckName.OpenDeckCard)
                {
                    if (targetCard.Value == 0) return matchState = DeckMatch.Found;
                    else return matchState = DeckMatch.NotFound;
                }

                if (targetDeck == DeckName.OpenDeckCard) return matchState = DeckMatch.Found;

                matchState = targetCard.Shape == _moveableCard.Shape ? DeckMatch.Found : DeckMatch.NotFound;
                if (matchState == DeckMatch.NotFound) return matchState;

                matchState = targetCard.Value == _moveableCard.Value - 1 ? DeckMatch.Found : DeckMatch.NotFound;
            }
            else if (_openDecks.ContainsKey(targetDeck))
            {
                // TODO..
            }

            return matchState;
        }

        public void DropCard(DeckName targetDeck)
        {
            if (_closedDecks.ContainsKey(targetDeck))
            {
                _closedDecks[targetDeck].Add(_moveableCard);
                var dropedCard = _closedDecks[targetDeck].Last();
                _moveableCard = null;

                switch (targetDeck)
                {
                    case DeckName.OpenDeckCard:
                        OpenDeckCard = dropedCard;
                        break;
                    case DeckName.DiamondsDeckCard:
                        DiamondsDeckCard = dropedCard;
                        break;
                    case DeckName.HeartsDeckCard:
                        HeartsDeckCard = dropedCard;
                        break;
                    case DeckName.SpadesDeckCard:
                        SpadesDeckCard = dropedCard;
                        break;
                    case DeckName.ClubsDeckCard:
                        ClubsDeckCard = dropedCard;
                        break;
                }
            }
            else if (_openDecks.ContainsKey(targetDeck))
            {
                // TODO..
            }
        }

        private void DealCard()
        {
            if (MainDeckCard.Path == Properties.Resources.BackCardPath)
            {
                MainDeckCard = _closedDecks[DeckName.MainDeckCard].Last();
                return;
            }
            else if (MainDeckCard.Path != Properties.Resources.EmptyCardPath)
            {
                OpenDeckCard = MainDeckCard;
                _closedDecks[DeckName.MainDeckCard].Remove(MainDeckCard);
                _closedDecks[DeckName.OpenDeckCard].Add(OpenDeckCard);
                MainDeckCard = _closedDecks[DeckName.MainDeckCard].LastOrDefault() ?? new CardViewModel(_transparentCard);
                return;
            }
            else
            {
                OpenDeckCard = new CardViewModel() { Path = string.Empty };
                _closedDecks[DeckName.OpenDeckCard].ForEach(op => _closedDecks[DeckName.MainDeckCard].Add(op));
                _closedDecks[DeckName.OpenDeckCard].Clear();
                MainDeckCard = _backCard;
            }
        }

        private List<CardViewModel> CreateMainDeck()
        {
            var mainCards = new List<CardViewModel>();

            mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Ace, Value = 1, Path = "/Images/Spades/AceOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Two, Value = 2, Path = "/Images/Spades/TwoOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Three, Value = 3, Path = "/Images/Spades/ThreeOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Four, Value = 4, Path = "/Images/Spades/FourOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Five, Value = 5, Path = "/Images/Spades/FiveOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Six, Value = 6, Path = "/Images/Spades/SixOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Seven, Value = 7, Path = "/Images/Spades/SevenOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Eight, Value = 8, Path = "/Images/Spades/EightOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Nine, Value = 9, Path = "/Images/Spades/NineOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Ten, Value = 10, Path = "/Images/Spades/TenOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Jack, Value = 11, Path = "/Images/Spades/JackOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.Queen, Value = 12, Path = "/Images/Spades/QueenOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Spades, Name = CardName.King, Value = 13, Path = "/Images/Spades/KingOfSpades.jpg" });

            mainCards.Add(new CardViewModel() { Shape = CardShape.Hearts, Name = CardName.Ace, Value = 1, Path = "/Images/Hearts/AceOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Hearts, Name = CardName.Two, Value = 2, Path = "/Images/Hearts/TwoOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Hearts, Name = CardName.Three, Value = 3, Path = "/Images/Hearts/ThreeOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Hearts, Name = CardName.Four, Value = 4, Path = "/Images/Hearts/FourOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Hearts, Name = CardName.Five, Value = 5, Path = "/Images/Hearts/FiveOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Hearts, Name = CardName.Six, Value = 6, Path = "/Images/Hearts/SixOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Hearts, Name = CardName.Seven, Value = 7, Path = "/Images/Hearts/SevenOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Hearts, Name = CardName.Eight, Value = 8, Path = "/Images/Hearts/EightOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Hearts, Name = CardName.Nine, Value = 9, Path = "/Images/Hearts/NineOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Hearts, Name = CardName.Ten, Value = 10, Path = "/Images/Hearts/TenOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Hearts, Name = CardName.Jack, Value = 11, Path = "/Images/Hearts/JackOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Hearts, Name = CardName.Queen, Value = 12, Path = "/Images/Hearts/QueenOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Hearts, Name = CardName.King, Value = 13, Path = "/Images/Hearts/KingOfHearts.jpg" });

            mainCards.Add(new CardViewModel() { Shape = CardShape.Clubs, Name = CardName.Ace, Value = 1, Path = "/Images/Clubs/AceOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Clubs, Name = CardName.Two, Value = 2, Path = "/Images/Clubs/TwoOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Clubs, Name = CardName.Three, Value = 3, Path = "/Images/Clubs/ThreeOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Clubs, Name = CardName.Four, Value = 4, Path = "/Images/Clubs/FourOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Clubs, Name = CardName.Five, Value = 5, Path = "/Images/Clubs/FiveOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Clubs, Name = CardName.Six, Value = 6, Path = "/Images/Clubs/SixOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Clubs, Name = CardName.Seven, Value = 7, Path = "/Images/Clubs/SevenOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Clubs, Name = CardName.Eight, Value = 8, Path = "/Images/Clubs/EightOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Clubs, Name = CardName.Nine, Value = 9, Path = "/Images/Clubs/NineOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Clubs, Name = CardName.Ten, Value = 10, Path = "/Images/Clubs/TenOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Clubs, Name = CardName.Jack, Value = 11, Path = "/Images/Clubs/JackOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Clubs, Name = CardName.Queen, Value = 12, Path = "/Images/Clubs/QueenOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Clubs, Name = CardName.King, Value = 13, Path = "/Images/Clubs/KingOfClubs.jpg" });

            mainCards.Add(new CardViewModel() { Shape = CardShape.Diamonds, Name = CardName.Ace, Value = 1, Path = "/Images/Diamonds/AceOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Diamonds, Name = CardName.Two, Value = 2, Path = "/Images/Diamonds/TwoOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Diamonds, Name = CardName.Three, Value = 3, Path = "/Images/Diamonds/ThreeOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Diamonds, Name = CardName.Four, Value = 4, Path = "/Images/Diamonds/FourOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Diamonds, Name = CardName.Five, Value = 5, Path = "/Images/Diamonds/FiveOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Diamonds, Name = CardName.Six, Value = 6, Path = "/Images/Diamonds/SixOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Diamonds, Name = CardName.Seven, Value = 7, Path = "/Images/Diamonds/SevenOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Diamonds, Name = CardName.Eight, Value = 8, Path = "/Images/Diamonds/EightOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Diamonds, Name = CardName.Nine, Value = 9, Path = "/Images/Diamonds/NineOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Diamonds, Name = CardName.Ten, Value = 10, Path = "/Images/Diamonds/TenOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Diamonds, Name = CardName.Jack, Value = 11, Path = "/Images/Diamonds/JackOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Diamonds, Name = CardName.Queen, Value = 12, Path = "/Images/Diamonds/QueenOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { Shape = CardShape.Diamonds, Name = CardName.King, Value = 13, Path = "/Images/Diamonds/KingOfDiamonds.jpg" });

            mainCards.Shuffle();

            return mainCards;
        }

        private void CreateClosedDecks()
        {
            _closedDecks = new Dictionary<DeckName, List<CardViewModel>>();

            _closedDecks[DeckName.MainDeckCard] = CreateMainDeck();
            MainDeckCard = _backCard;

            _closedDecks[DeckName.OpenDeckCard] = new List<CardViewModel>() { new CardViewModel(_transparentCard) };
            OpenDeckCard = _closedDecks[DeckName.OpenDeckCard].Last();

            _closedDecks[DeckName.DiamondsDeckCard] = new List<CardViewModel>() { new CardViewModel(_transparentCard) };
            _closedDecks[DeckName.DiamondsDeckCard].Last().Shape = CardShape.Diamonds;
            DiamondsDeckCard = _closedDecks[DeckName.DiamondsDeckCard].Last();

            _closedDecks[DeckName.HeartsDeckCard] = new List<CardViewModel>() { new CardViewModel(_transparentCard) };
            _closedDecks[DeckName.HeartsDeckCard].Last().Shape = CardShape.Hearts;
            HeartsDeckCard = _closedDecks[DeckName.HeartsDeckCard].Last();

            _closedDecks[DeckName.ClubsDeckCard] = new List<CardViewModel>() { new CardViewModel(_transparentCard) };
            _closedDecks[DeckName.ClubsDeckCard].Last().Shape = CardShape.Clubs;
            ClubsDeckCard = _closedDecks[DeckName.ClubsDeckCard].Last();

            _closedDecks[DeckName.SpadesDeckCard] = new List<CardViewModel>() { new CardViewModel(_transparentCard) };
            _closedDecks[DeckName.SpadesDeckCard].Last().Shape = CardShape.Spades;
            SpadesDeckCard = _closedDecks[DeckName.SpadesDeckCard].Last();
        }

        private void CreateOpenDecks()
        {
            _openDecks = new Dictionary<DeckName, List<ContainerViewModel>>();

            SetContainer(DeckName.FirstDeck, 2);
            FirstDeckCards = _openDecks[DeckName.FirstDeck];

            SetContainer(DeckName.SecondDeck, 3);
            SecondDeckCards = _openDecks[DeckName.SecondDeck];

            SetContainer(DeckName.ThirdDeck, 4);
            ThirdDeckCards = _openDecks[DeckName.ThirdDeck];

            SetContainer(DeckName.FourthDeck, 5);
            FourthDeckCards = _openDecks[DeckName.FourthDeck];

            SetContainer(DeckName.FifthDeck, 6);
            FifthDeckCards = _openDecks[DeckName.FifthDeck];

            SetContainer(DeckName.SixthDeck, 7);
            SixthDeckCards = _openDecks[DeckName.SixthDeck];

            SetContainer(DeckName.SeventhDeck, 8);
            SeventhDeckCards = _openDecks[DeckName.SeventhDeck];
        }

        private void SetContainer(DeckName newDeck, int cardsCount)
        {
            _openDecks[newDeck] = new List<ContainerViewModel>();

            for (int i = 1; i <= cardsCount; i++)
            {
                var randomCard = _closedDecks[DeckName.MainDeckCard].Last();
                randomCard.CurrentDeck = newDeck;
                string cardPath = Properties.Resources.BackCardPath;
                if (i == cardsCount) cardPath = randomCard.Path;
                var container = new ContainerViewModel()
                {
                    CardPath = cardPath,
                    FrontCardPath = randomCard.Path,
                    CardName = randomCard.Name.Value,
                    CardShape = randomCard.Shape.Value,
                    CardValue = randomCard.Value,
                    DeckName = randomCard.CurrentDeck,
                    TakeCardEventResource = TakeCardEventResource,
                };
                _openDecks[newDeck].Add(container);
                _closedDecks[DeckName.MainDeckCard].Remove(randomCard);
            }
        }
    }
}
