using Solitare.UI.Controls.Canvas;
using Solitare.UI.Controls.Image;
using Solitare.UI.Enums;
using Solitare.UI.Game.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Solitare.UI.Game.Views
{
    public partial class GameView : UserControl
    {
        private GameViewModel _gameViewModel;

        private DropData _currentDrag;
        private bool _isDrag;

        private List<CardContainer> _closedDecks;
        private List<CardContainer> _openDecks;
        private Canvas _mainCanvas;
        private Card _moveableCard;
        private CardContainer _moveableContainer;

        public GameView()
        {
            InitializeComponent();

            _mainCanvas = MainCanvas;
            _closedDecks = new List<CardContainer>() { OpenDeckCard, DiamondsDeckCard, ClubsDeckCard, HeartsDeckCard, SpadesDeckCard };
            _openDecks = new List<CardContainer>() { FirstDeck, SecondDeck, ThirdDeck, FourthDeck, FifthDeck, SixthDeck, SeventhDeck };

            AddHandler(LoadedEvent, new RoutedEventHandler(OnLoadEvent));
            AddHandler(MouseMoveEvent, new MouseEventHandler(OnMouseMoveChanged));
        }

        private void OnLoadEvent(object sender, EventArgs e)
        {
            _gameViewModel = (GameViewModel)DataContext;
        }

        private void OnMouseMoveChanged(object sender, MouseEventArgs args)
        {
            if (_isDrag)
            {
                SetMoveableCardPosition(args);

                _closedDecks.ForEach(d => FindOverDeck(d, args));

                _openDecks.ForEach(d => FindOverCard(d, args));
            }
        }

        private void DropMoveableCard(object sender, MouseButtonEventArgs args)
        {
            if (_moveableCard.isOverOpenDeck) DropCard(OpenDeckCard);
            else if (_moveableCard.IsOverDiamondsDeck) DropCard(DiamondsDeckCard);
            else if (_moveableCard.IsOverClubsDeck) DropCard(ClubsDeckCard);
            else if (_moveableCard.IsOverHeartsDeck) DropCard(HeartsDeckCard);
            else if (_moveableCard.IsOverSpadesDeck) DropCard(SpadesDeckCard);
            else if (_moveableCard.IsOverFirstDeck) DropCard(FirstDeck);
            else if (_moveableCard.IsOverSecondDeck) DropCard(SecondDeck);
            else if (_moveableCard.IsOverThirdDeck) DropCard(ThirdDeck);
            else if (_moveableCard.IsOverFourthDeck) DropCard(FourthDeck);
            else if (_moveableCard.IsOverFifthDeck) DropCard(FifthDeck);
            else if (_moveableCard.IsOverSixthDeck) DropCard(SixthDeck);
            else if (_moveableCard.IsOverSeventhDeck) DropCard(SeventhDeck);
        }

        private void DropMoveableContainer(object sender, MouseButtonEventArgs args)
        {
            if (_moveableContainer.IsOverFirstDeck) DropCard(FirstDeck);
            else if (_moveableContainer.IsOverSecondDeck) DropCard(SecondDeck);
            else if (_moveableContainer.IsOverThirdDeck) DropCard(ThirdDeck);
            else if (_moveableContainer.IsOverFourthDeck) DropCard(FourthDeck);
            else if (_moveableContainer.IsOverFifthDeck) DropCard(FifthDeck);
            else if (_moveableContainer.IsOverSixthDeck) DropCard(SixthDeck);
            else if (_moveableContainer.IsOverSeventhDeck) DropCard(SeventhDeck);
        }

        private void DropCard(CardContainer deck)
        {
            _gameViewModel.DropCard(deck.ContainerName);

            deck.Background = null;
            if (_moveableContainer != null)
            {
                _mainCanvas.Children.Remove(_moveableContainer);
                _moveableContainer = null;
            }
            else
            {
                _mainCanvas.Children.Remove(_moveableCard);
                _moveableCard = null;
            }
            _isDrag = false;
        }

        private void FindOverDeck(CardContainer deck, MouseEventArgs args)
        {
            var point = deck.TransformToAncestor(Application.Current.MainWindow)
                                                            .Transform(new Point(0, 0));

            if (args.GetPosition(_mainCanvas).X >= point.X && args.GetPosition(_mainCanvas).X <= point.X + deck.ActualWidth &&
                args.GetPosition(_mainCanvas).Y >= point.Y - 70 && args.GetPosition(_mainCanvas).Y <= point.Y + deck.ActualHeight + 70)
            {
                if (_gameViewModel.ValidateDeck(deck.ContainerName) == DeckMatch.NotFound) return;

                SetIsMouseOver(deck, true);
            }
            else
            {
                SetIsMouseOver(deck, false);
            }
        }

        private void FindOverCard(CardContainer deck, MouseEventArgs args)
        {
            var frontCard = FindFrontCard(deck);
        
            var point = frontCard.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));

            if (args.GetPosition(_mainCanvas).X >= point.X && args.GetPosition(_mainCanvas).X <= point.X + deck.ActualWidth &&
                args.GetPosition(_mainCanvas).Y >= point.Y - 70 && args.GetPosition(_mainCanvas).Y <= point.Y + deck.ActualHeight + 70)
            {
                if (_gameViewModel.ValidateCard(frontCard.CurrentDeck, frontCard.CardName, frontCard.CardShape, frontCard.CardValue) == DeckMatch.NotFound) return;
                
                SetIsMouseOver(deck, true, frontCard);
            }
            else
            {
                SetIsMouseOver(deck, false, frontCard);
            }
        }

        private Card FindFrontCard(CardContainer deck)
        {
            Card card = null;
            foreach (var child in deck.Children)
            {
                if (child is CardContainer)
                {
                    card = FindFrontCard((CardContainer)child);
                }
                else
                {
                    card = (Card)child;
                }
            }
            return card;
        }

        private void SetIsMouseOver(CardContainer deck, bool isOver, Card card = null)
        {
            switch (deck.ContainerName)
            {
                case DeckName.OpenDeckCard:
                    deck.Background = isOver ? Brushes.Red : null;
                    _moveableCard.isOverOpenDeck = isOver;
                    break;
                case DeckName.DiamondsDeckCard:
                    deck.Background = isOver ? Brushes.Black : null;
                    _moveableCard.IsOverDiamondsDeck = isOver;
                    break;
                case DeckName.SpadesDeckCard:
                    deck.Background = isOver ? Brushes.Red : null;
                    _moveableCard.IsOverSpadesDeck = isOver;
                    break;
                case DeckName.HeartsDeckCard:
                    deck.Background = isOver ? Brushes.Black : null;
                    _moveableCard.IsOverHeartsDeck = isOver;
                    break;
                case DeckName.ClubsDeckCard:
                    deck.Background = isOver ? Brushes.Red : null;
                    _moveableCard.IsOverClubsDeck = isOver;
                    break;

                case DeckName.FirstDeck:
                    SetCardVisualization(deck, card, isOver);
                    if (_moveableContainer != null)
                    {
                        _moveableContainer.IsOverFirstDeck = isOver;
                        break;
                    }
                    _moveableCard.IsOverFirstDeck = isOver;
                    break;
                case DeckName.SecondDeck:
                    SetCardVisualization(deck, card, isOver);
                    if (_moveableContainer != null)
                    {
                        _moveableContainer.IsOverSecondDeck = isOver;
                        break;
                    }
                    _moveableCard.IsOverSecondDeck = isOver;
                    break;
                case DeckName.ThirdDeck:
                    SetCardVisualization(deck, card, isOver);
                    if (_moveableContainer != null)
                    {
                        _moveableContainer.IsOverThirdDeck = isOver;
                        break;
                    }
                    _moveableCard.IsOverThirdDeck = isOver;
                    break;
                case DeckName.FourthDeck:
                    SetCardVisualization(deck, card, isOver);
                    if (_moveableContainer != null)
                    {
                        _moveableContainer.IsOverFourthDeck = isOver;
                        break;
                    }
                    _moveableCard.IsOverFourthDeck = isOver;
                    break;
                case DeckName.FifthDeck:
                    SetCardVisualization(deck, card, isOver);
                    if (_moveableContainer != null)
                    {
                        _moveableContainer.IsOverFifthDeck = isOver;
                        break;
                    }
                    _moveableCard.IsOverFifthDeck = isOver;
                    break;
                case DeckName.SixthDeck:
                    SetCardVisualization(deck, card, isOver);
                    if (_moveableContainer != null)
                    {
                        _moveableContainer.IsOverSixthDeck = isOver;
                        break;
                    }
                    _moveableCard.IsOverSixthDeck = isOver;
                    break;
                case DeckName.SeventhDeck:
                    SetCardVisualization(deck, card, isOver);
                    if (_moveableContainer != null)
                    {
                        _moveableContainer.IsOverSeventhDeck = isOver;
                        break;
                    }
                    _moveableCard.IsOverSeventhDeck = isOver;
                    break;
            }
        }

        private void SetCardVisualization(CardContainer deck, Card card, bool isOver)
        {
            if (card == null) throw new NullReferenceException();
            if (card.Path == Properties.Resources.EmptyCardPath)
            {
                deck.Background = isOver ? Brushes.Blue : null;
            }
            else card.Opacity = isOver ? 0.7 : 1;
        }

        public void TakeCard(object sender, MouseButtonEventArgs args)
        {
            var cardBase = args.Source as Card;

            /*
            foreach (var child in _mainCanvas.Children)
            {
                if (child is Card)
                {
                    var card = (Card)child;
                    if (card.Name == cardBase.Name && card.CardShape == cardBase.CardShape) return;
                }
            }*/

            if (cardBase.CardValue == 0) return;

            if (cardBase.Path == Properties.Resources.BackCardPath)
            {
                _gameViewModel.SetFllipedCardBinding(cardBase.CurrentDeck, cardBase.CardName, cardBase.CardShape);
                return;
            }

            var cardContainer = _openDecks.Find(d => d.ContainerName == cardBase.CurrentDeck);
            var fllipedCardCount = cardContainer.ContainersSource.Count(c => c.CardPath != Properties.Resources.BackCardPath && c.CardPath != Properties.Resources.EmptyCardPath);
            if (fllipedCardCount > 1)
            {
                SetMoveableContainer(cardContainer, args);
                return;
            }

            _moveableCard = new Card(cardBase);
            _moveableCard.MouseLeftButtonDown += DropMoveableCard;

            _gameViewModel.SetMoveableCardBinding(_moveableCard.CardName, _moveableCard.CardShape, _moveableCard.CardValue, _moveableCard.CurrentDeck, _moveableCard.Path);

            _isDrag = true;

            SetMoveableCardPosition(args);
            _mainCanvas.Children.Add(_moveableCard);

            _currentDrag = new DropData(_moveableCard.CurrentDeck, _moveableCard);
        }

        private void SetMoveableCardPosition(MouseEventArgs args)
        {
            FrameworkElement moveable = null;
            if (_moveableCard != null)
            {
                moveable = _moveableCard;
            }
            else moveable = _moveableContainer;

            Canvas.SetLeft(moveable, args.GetPosition(_mainCanvas).X - moveable.ActualWidth / 2);
            Canvas.SetTop(moveable, args.GetPosition(_mainCanvas).Y - moveable.ActualHeight / 2);
        }

        private void SetMoveableContainer(CardContainer cardContainer, MouseEventArgs args)
        {
            _moveableContainer = cardContainer;
            _moveableContainer.MouseLeftButtonDown += DropMoveableContainer;

            var firstContainer = _openDecks.Find(d => d.ContainerName == _moveableContainer.ContainerName);
            _gameViewModel.SetMoveableContainerBinding(SetContainersList(firstContainer));

            _isDrag = true;

            SetMoveableCardPosition(args);
            var parent = _moveableContainer.Parent;
            _mainCanvas.Children.Add(_moveableContainer);

            _currentDrag = new DropData(_moveableContainer.ContainerName, _moveableContainer.Card);
        }

        private List<ContainerViewModel> SetContainersList(CardContainer container)
        {
            var containers = new List<ContainerViewModel>();

            ContainerViewModel containerViewModel = new ContainerViewModel()
            {
                CardName = container.Card.CardName,
                CardShape = container.Card.CardShape,
                CardValue = container.Card.CardValue,
                DeckName = container.Card.CurrentDeck,
                CardPath = container.Card.Path,
            };
            containers.Add(containerViewModel);

            if (container.SubContainer != null)
            {
                containers = SetContainersList(container.SubContainer);

                var currentIndex = containers.IndexOf(containerViewModel);
                containerViewModel.SubContainer = containers.ElementAt(currentIndex + 1);
            }

            return containers;
        }
    }

    public class DropData
    {
        public DropData(DeckName sourceDeck, Card movedCard)
        {
            SourceDeck = sourceDeck;
            MovedCard = movedCard;
        }

        public DeckName SourceDeck { get; set; }

        public Card MovedCard { get; set; }
    }
}
