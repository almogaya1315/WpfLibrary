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
using System.Timers;
using Solitare.UI.Options.ViewModels;
using Solitare.UI.Menu.ViewModels;

namespace Solitare.UI.Game.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        readonly private MainViewModel _mainViewModel;
        readonly private MenuViewModel _menuViewModel;

        private CardViewModel _backCard;
        private CardViewModel _transparentCard;
        private CardViewModel _moveableCard;
        private ContainerViewModel _transparentContainer;
        private ContainerViewModel _moveableContainer;

        private Dictionary<DeckName, List<CardViewModel>> _closedDecks;
        private Dictionary<DeckName, List<ContainerViewModel>> _openDecks;

        private string _currentTimer;
        public string CurrentTimer
        {
            get { return _currentTimer; }
            set
            {
                _currentTimer = value;
                RaisePropertyChanged();
            }
        }
        public Visibility UndoBtnVisibility { get; set; }

        public ICommand Deal { get; set; }
        public ICommand Reset { get; set; }
        public ICommand Back { get; set; }
        public ICommand Undo { get; set; }
        private bool _undoBtnEnabled;
        public bool UndoBtnEnabled
        {
            get { return _undoBtnEnabled; }
            set
            {
                _undoBtnEnabled = value;
                RaisePropertyChanged();
            }
        }

        private CardViewModel _lastMovedCard;
        private ContainerViewModel _lastSourceContainer;

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

        private Timer _timer;
        private TimeSpan _timeSpan;
        private RuleSetViewModel _ruleSet { get; set; }

        public GameViewModel(MainViewModel mainViewModel, MenuViewModel menuViewModel, RuleSetViewModel ruleSet)
        {
            _mainViewModel = mainViewModel;
            _menuViewModel = menuViewModel;
            _ruleSet = ruleSet;

            _transparentCard = new CardViewModel() { CardPath = Properties.Resources.EmptyCardPath };
            _transparentContainer = new ContainerViewModel() { CardPath = _transparentCard.CardPath };
            _backCard = new CardViewModel() { CardPath = Properties.Resources.BackCardPath };

            TakeCardEventResource = new EventResource(EventName.MouseLeftButtonDownEvent);

            CreateClosedDecks();
            CreateOpenDecks();

            Deal = new RelayCommand(DealCard);
            Reset = new RelayCommand(ResetGame);
            Back = new RelayCommand(BackToMenu);
            Undo = new RelayCommand(UndoLastCard);

            if (_ruleSet.TimerEnabled)
            {
                _timer = new Timer(1000);
                _timer.Elapsed += _timer_Elapsed;
                _timeSpan = TimeSpan.FromSeconds(1);
                _timer.Start();
                _timer_Elapsed(this, null);
            }

            UndoBtnEnabled = false;
            if (!_ruleSet.UndoEnabled)
            {
                UndoBtnVisibility = Visibility.Hidden;
            }
        }

        private void UndoLastCard()
        {

        }

        private void BackToMenu()
        {
            var result = MessageBox.Show("Are you sure?", "Go back!", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel) return;

            _mainViewModel.SwitchToMenuView(_menuViewModel);
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CurrentTimer = _timeSpan.ToString();
            _timeSpan = _timeSpan.Add(TimeSpan.FromSeconds(1));
        }

        private void ResetGame()
        {
            var result = MessageBox.Show("Are you sure?", "Reset game!", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel) return;

            CreateClosedDecks();
            CreateOpenDecks();

            if (_ruleSet.TimerEnabled)
            {
                _timeSpan = TimeSpan.FromSeconds(1);
                _timer_Elapsed(this, null);
            }
            UndoBtnEnabled = false;
        }

        public void SetMoveableCardBinding(CardName cardName, CardShape cardShape, int cardValue, DeckName sourceDeck, string path)
        {
            _moveableCard = new CardViewModel() { CardName = cardName, CardShape = cardShape, CardValue = cardValue, CardPath = path, CurrentDeck = sourceDeck };

            if (_closedDecks.ContainsKey(sourceDeck))
            {
                _closedDecks[sourceDeck].RemoveAll(c => c.CardName == cardName && c.CardShape == cardShape);
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
                if (_openDecks[sourceDeck].LastOrDefault() != null)
                {
                    _openDecks[sourceDeck].Last().SubContainer = null;
                }

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

        public void SetMoveableContainerBinding(List<ContainerViewModel> moveableCardsSet)
        {
            DeckName? currentDeck = null;
            foreach (var container in moveableCardsSet)
            {
                _openDecks[container.DeckName].RemoveAll(c => c.CardName == container.CardName && c.CardShape == container.CardShape);
                currentDeck = container.DeckName;
            }

            if (!currentDeck.HasValue) throw new Exception();
            if (_openDecks[currentDeck.Value].LastOrDefault() != null)
            {
                _openDecks[currentDeck.Value].Last().SubContainer = null;
            }
            _moveableContainer = moveableCardsSet.First();
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
            }
        }

        public DeckMatch ValidateDeck(DeckName targetDeck)
        {
            DeckMatch matchState = DeckMatch.NotFound;

            if (_closedDecks.ContainsKey(targetDeck))
            {
                var targetCard = _closedDecks[targetDeck].LastOrDefault();

                if (targetDeck == DeckName.OpenDeckCard)
                {
                    return matchState = DeckMatch.Found;
                }

                matchState = targetCard.CardShape == _moveableCard.CardShape ? DeckMatch.Found : DeckMatch.NotFound;
                if (matchState == DeckMatch.NotFound) return matchState;

                matchState = targetCard.CardValue == _moveableCard.CardValue - 1 ? DeckMatch.Found : DeckMatch.NotFound;
            }
            else throw new KeyNotFoundException();

            return matchState;
        }

        public DeckMatch ValidateCard(DeckName targetDeck, CardName? cardName, CardShape? cardShape, int cardValue)
        {
            if (_moveableContainer != null)
            {
                if (_openDecks.ContainsKey(targetDeck))
                {
                    return ValidateCard(targetDeck, _moveableContainer.CardShape.Value, _moveableContainer.CardValue, cardName, cardShape, cardValue);
                }
            }
            else if (_openDecks.ContainsKey(targetDeck))
            {
                return ValidateCard(targetDeck, _moveableCard.CardShape.Value, _moveableCard.CardValue, cardName, cardShape, cardValue);
            }

            throw new KeyNotFoundException();
        }

        private DeckMatch ValidateCard(DeckName targetDeck, CardShape moveableItemCardShape, int moveableItemCardValue, CardName? cardName, CardShape? cardShape, int cardValue)
        {
            DeckMatch matchState = DeckMatch.NotFound;

            if (cardName == null && cardShape == null) return matchState = DeckMatch.Found;

            var targetCard = _openDecks[targetDeck].Find(c => (c.CardName == cardName && c.CardShape == cardShape));
            if (targetCard == null && moveableItemCardValue == 13) return matchState = DeckMatch.Found;
            if (targetCard == null) return matchState = DeckMatch.NotFound;

            if (targetCard.CardPath == Properties.Resources.BackCardPath) return matchState = DeckMatch.Found;

            //if (targetCard.CardPath == Properties.Resources.EmptyCardPath && moveableItemCardValue == 14) return matchState = DeckMatch.Found;

            if ((moveableItemCardShape == CardShape.Hearts || moveableItemCardShape == CardShape.Diamonds) &&
                (targetCard.CardShape == CardShape.Clubs || targetCard.CardShape == CardShape.Spades))
            {
                matchState = DeckMatch.Found;
            }
            else if ((moveableItemCardShape == CardShape.Clubs || moveableItemCardShape == CardShape.Spades) &&
                     (targetCard.CardShape == CardShape.Hearts || targetCard.CardShape == CardShape.Diamonds))
            {
                matchState = DeckMatch.Found;
            }
            else return DeckMatch.NotFound;

            if (targetCard.CardValue - moveableItemCardValue == 1)
            {
                matchState = DeckMatch.Found;
            }
            else matchState = DeckMatch.NotFound;

            return matchState;
        }

        public void DropCard(DeckName targetDeck)
        {
            if (_closedDecks.ContainsKey(targetDeck))
            {
                _closedDecks[targetDeck].Add(_moveableCard);
                var dropedCard = _closedDecks[targetDeck].Last();
                _lastMovedCard = dropedCard;
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
                if (_moveableContainer != null)
                {
                    _openDecks[targetDeck] = SetDeckCards(_openDecks[targetDeck], targetDeck, _moveableContainer);

                    // TODO: maintain _moveableContainer.Card
                    //_lastMovedCard = _moveableContainer.Card;
                }
                else
                {
                    _openDecks[targetDeck] = SetDeckCards(_openDecks[targetDeck], targetDeck, _moveableCard);
                    //_lastMovedCard = _moveableCard;
                }

                switch (targetDeck)
                {
                    case DeckName.FirstDeck:
                        FirstDeckCards = new List<ContainerViewModel>(_openDecks[targetDeck]);
                        break;
                    case DeckName.SecondDeck:
                        SecondDeckCards = new List<ContainerViewModel>(_openDecks[targetDeck]);
                        break;
                    case DeckName.ThirdDeck:
                        ThirdDeckCards = new List<ContainerViewModel>(_openDecks[targetDeck]);
                        break;
                    case DeckName.FourthDeck:
                        FourthDeckCards = new List<ContainerViewModel>(_openDecks[targetDeck]);
                        break;
                    case DeckName.FifthDeck:
                        FifthDeckCards = new List<ContainerViewModel>(_openDecks[targetDeck]);
                        break;
                    case DeckName.SixthDeck:
                        SixthDeckCards = new List<ContainerViewModel>(_openDecks[targetDeck]);
                        break;
                    case DeckName.SeventhDeck:
                        SeventhDeckCards = new List<ContainerViewModel>(_openDecks[targetDeck]);
                        break;
                }
            }

            CheckGameStatus();
        }

        private List<ContainerViewModel> SetDeckCards(List<ContainerViewModel> deckCards, DeckName targetDeck, IMoveable moveableItem)
        {
            deckCards.Add(new ContainerViewModel()
            {
                CardPath = moveableItem.CardPath,
                FrontCardPath = moveableItem.CardPath,
                CardName = moveableItem.CardName.Value,
                CardShape = moveableItem.CardShape.Value,
                CardValue = moveableItem.CardValue,
                DeckName = targetDeck,
                TakeCardEventResource = TakeCardEventResource,
            });

            if (moveableItem is CardViewModel) _moveableCard = null;
            else if (moveableItem is ContainerViewModel)
            {
                if ((moveableItem as ContainerViewModel).SubContainer != null)
                {
                    deckCards = SetDeckCards(deckCards, targetDeck, (moveableItem as ContainerViewModel).SubContainer);
                }
                _moveableContainer = null;
            }

            return deckCards;
        }

        private void DealCard()
        {
            if (MainDeckCard.CardPath == Properties.Resources.BackCardPath)
            {
                MainDeckCard = _closedDecks[DeckName.MainDeckCard].Last();
                return;
            }
            else if (MainDeckCard.CardPath != Properties.Resources.EmptyCardPath)
            {
                OpenDeckCard = MainDeckCard;
                _closedDecks[DeckName.MainDeckCard].Remove(MainDeckCard);
                _closedDecks[DeckName.OpenDeckCard].Add(OpenDeckCard);
                MainDeckCard = _closedDecks[DeckName.MainDeckCard].LastOrDefault() ?? new CardViewModel(_transparentCard);
                return;
            }
            else
            {
                OpenDeckCard = new CardViewModel() { CardPath = string.Empty };
                _closedDecks[DeckName.OpenDeckCard].ForEach(op => _closedDecks[DeckName.MainDeckCard].Add(op));
                _closedDecks[DeckName.OpenDeckCard].Clear();
                MainDeckCard = _backCard;
            }
        }

        private void CheckGameStatus()
        {
            if (DiamondsDeckCard.CardName == CardName.King && HeartsDeckCard.CardName == CardName.King &&
                ClubsDeckCard.CardName == CardName.King && SpadesDeckCard.CardName == CardName.King)
            {
                var result = MessageBox.Show("Great work!" + Environment.NewLine + "Deal again?", "Game completed!", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    CreateClosedDecks();
                    CreateOpenDecks();

                    if (_ruleSet.TimerEnabled)
                    {
                        _timeSpan = TimeSpan.FromSeconds(1);
                        _timer_Elapsed(this, null);
                    }
                    UndoBtnEnabled = false;
                    return;
                }
                else
                {
                    _mainViewModel.SwitchToMenuView(_menuViewModel);
                }
            }
        }

        private List<CardViewModel> CreateMainDeck()
        {
            var mainCards = new List<CardViewModel>();

            mainCards.Add(new CardViewModel() { CardShape = CardShape.Spades, CardName = CardName.Ace, CardValue = 1, CardPath = "/Images/Spades/AceOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Spades, CardName = CardName.Two, CardValue = 2, CardPath = "/Images/Spades/TwoOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Spades, CardName = CardName.Three, CardValue = 3, CardPath = "/Images/Spades/ThreeOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Spades, CardName = CardName.Four, CardValue = 4, CardPath = "/Images/Spades/FourOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Spades, CardName = CardName.Five, CardValue = 5, CardPath = "/Images/Spades/FiveOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Spades, CardName = CardName.Six, CardValue = 6, CardPath = "/Images/Spades/SixOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Spades, CardName = CardName.Seven, CardValue = 7, CardPath = "/Images/Spades/SevenOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Spades, CardName = CardName.Eight, CardValue = 8, CardPath = "/Images/Spades/EightOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Spades, CardName = CardName.Nine, CardValue = 9, CardPath = "/Images/Spades/NineOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Spades, CardName = CardName.Ten, CardValue = 10, CardPath = "/Images/Spades/TenOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Spades, CardName = CardName.Jack, CardValue = 11, CardPath = "/Images/Spades/JackOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Spades, CardName = CardName.Queen, CardValue = 12, CardPath = "/Images/Spades/QueenOfSpades.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Spades, CardName = CardName.King, CardValue = 13, CardPath = "/Images/Spades/KingOfSpades.jpg" });

            mainCards.Add(new CardViewModel() { CardShape = CardShape.Hearts, CardName = CardName.Ace, CardValue = 1, CardPath = "/Images/Hearts/AceOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Hearts, CardName = CardName.Two, CardValue = 2, CardPath = "/Images/Hearts/TwoOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Hearts, CardName = CardName.Three, CardValue = 3, CardPath = "/Images/Hearts/ThreeOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Hearts, CardName = CardName.Four, CardValue = 4, CardPath = "/Images/Hearts/FourOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Hearts, CardName = CardName.Five, CardValue = 5, CardPath = "/Images/Hearts/FiveOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Hearts, CardName = CardName.Six, CardValue = 6, CardPath = "/Images/Hearts/SixOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Hearts, CardName = CardName.Seven, CardValue = 7, CardPath = "/Images/Hearts/SevenOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Hearts, CardName = CardName.Eight, CardValue = 8, CardPath = "/Images/Hearts/EightOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Hearts, CardName = CardName.Nine, CardValue = 9, CardPath = "/Images/Hearts/NineOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Hearts, CardName = CardName.Ten, CardValue = 10, CardPath = "/Images/Hearts/TenOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Hearts, CardName = CardName.Jack, CardValue = 11, CardPath = "/Images/Hearts/JackOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Hearts, CardName = CardName.Queen, CardValue = 12, CardPath = "/Images/Hearts/QueenOfHearts.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Hearts, CardName = CardName.King, CardValue = 13, CardPath = "/Images/Hearts/KingOfHearts.jpg" });

            mainCards.Add(new CardViewModel() { CardShape = CardShape.Clubs, CardName = CardName.Ace, CardValue = 1, CardPath = "/Images/Clubs/AceOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Clubs, CardName = CardName.Two, CardValue = 2, CardPath = "/Images/Clubs/TwoOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Clubs, CardName = CardName.Three, CardValue = 3, CardPath = "/Images/Clubs/ThreeOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Clubs, CardName = CardName.Four, CardValue = 4, CardPath = "/Images/Clubs/FourOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Clubs, CardName = CardName.Five, CardValue = 5, CardPath = "/Images/Clubs/FiveOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Clubs, CardName = CardName.Six, CardValue = 6, CardPath = "/Images/Clubs/SixOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Clubs, CardName = CardName.Seven, CardValue = 7, CardPath = "/Images/Clubs/SevenOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Clubs, CardName = CardName.Eight, CardValue = 8, CardPath = "/Images/Clubs/EightOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Clubs, CardName = CardName.Nine, CardValue = 9, CardPath = "/Images/Clubs/NineOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Clubs, CardName = CardName.Ten, CardValue = 10, CardPath = "/Images/Clubs/TenOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Clubs, CardName = CardName.Jack, CardValue = 11, CardPath = "/Images/Clubs/JackOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Clubs, CardName = CardName.Queen, CardValue = 12, CardPath = "/Images/Clubs/QueenOfClubs.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Clubs, CardName = CardName.King, CardValue = 13, CardPath = "/Images/Clubs/KingOfClubs.jpg" });

            mainCards.Add(new CardViewModel() { CardShape = CardShape.Diamonds, CardName = CardName.Ace, CardValue = 1, CardPath = "/Images/Diamonds/AceOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Diamonds, CardName = CardName.Two, CardValue = 2, CardPath = "/Images/Diamonds/TwoOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Diamonds, CardName = CardName.Three, CardValue = 3, CardPath = "/Images/Diamonds/ThreeOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Diamonds, CardName = CardName.Four, CardValue = 4, CardPath = "/Images/Diamonds/FourOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Diamonds, CardName = CardName.Five, CardValue = 5, CardPath = "/Images/Diamonds/FiveOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Diamonds, CardName = CardName.Six, CardValue = 6, CardPath = "/Images/Diamonds/SixOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Diamonds, CardName = CardName.Seven, CardValue = 7, CardPath = "/Images/Diamonds/SevenOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Diamonds, CardName = CardName.Eight, CardValue = 8, CardPath = "/Images/Diamonds/EightOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Diamonds, CardName = CardName.Nine, CardValue = 9, CardPath = "/Images/Diamonds/NineOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Diamonds, CardName = CardName.Ten, CardValue = 10, CardPath = "/Images/Diamonds/TenOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Diamonds, CardName = CardName.Jack, CardValue = 11, CardPath = "/Images/Diamonds/JackOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Diamonds, CardName = CardName.Queen, CardValue = 12, CardPath = "/Images/Diamonds/QueenOfDiamonds.jpg" });
            mainCards.Add(new CardViewModel() { CardShape = CardShape.Diamonds, CardName = CardName.King, CardValue = 13, CardPath = "/Images/Diamonds/KingOfDiamonds.jpg" });

            mainCards.Shuffle();

            return mainCards;
        }

        private void CreateClosedDecks()
        {
            _closedDecks = new Dictionary<DeckName, List<CardViewModel>>();

            _closedDecks[DeckName.MainDeckCard] = CreateMainDeck();
            MainDeckCard = new CardViewModel(_backCard) { CurrentDeck = DeckName.MainDeckCard };

            _closedDecks[DeckName.OpenDeckCard] = new List<CardViewModel>() { new CardViewModel(_transparentCard) };
            OpenDeckCard = _closedDecks[DeckName.OpenDeckCard].Last();

            _closedDecks[DeckName.DiamondsDeckCard] = new List<CardViewModel>() { new CardViewModel(_transparentCard) };
            _closedDecks[DeckName.DiamondsDeckCard].Last().CardShape = CardShape.Diamonds;
            DiamondsDeckCard = _closedDecks[DeckName.DiamondsDeckCard].Last();

            _closedDecks[DeckName.HeartsDeckCard] = new List<CardViewModel>() { new CardViewModel(_transparentCard) };
            _closedDecks[DeckName.HeartsDeckCard].Last().CardShape = CardShape.Hearts;
            HeartsDeckCard = _closedDecks[DeckName.HeartsDeckCard].Last();

            _closedDecks[DeckName.ClubsDeckCard] = new List<CardViewModel>() { new CardViewModel(_transparentCard) };
            _closedDecks[DeckName.ClubsDeckCard].Last().CardShape = CardShape.Clubs;
            ClubsDeckCard = _closedDecks[DeckName.ClubsDeckCard].Last();

            _closedDecks[DeckName.SpadesDeckCard] = new List<CardViewModel>() { new CardViewModel(_transparentCard) };
            _closedDecks[DeckName.SpadesDeckCard].Last().CardShape = CardShape.Spades;
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
                if (i == cardsCount) cardPath = randomCard.CardPath;
                var container = new ContainerViewModel()
                {
                    CardPath = cardPath,
                    FrontCardPath = randomCard.CardPath,
                    CardName = randomCard.CardName.Value,
                    CardShape = randomCard.CardShape.Value,
                    CardValue = randomCard.CardValue,
                    DeckName = newDeck,
                    TakeCardEventResource = TakeCardEventResource,
                };
                _openDecks[newDeck].Add(container);
                _closedDecks[DeckName.MainDeckCard].Remove(randomCard);
            }
        }
    }
}
