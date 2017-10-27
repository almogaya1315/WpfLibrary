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
                SetMoveableItemPosition(args);

                if (_moveableContainer == null)
                {
                    _closedDecks.ForEach(d => FindOverDeck(d, args));
                }

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
            Point point;
            if (frontCard == null)
            {
                point = deck.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
                frontCard = new Card() { CurrentDeck = deck.ContainerName, CardName = null, CardShape = null, CardValue = 0 };
            }
            else
            {
                point = frontCard.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
            }

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
            if (_moveableCard == null && _moveableContainer == null) return;

            

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
                    if (!IsKingCardOnEmptyOpenDeck(deck)) return;

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

        private bool IsKingCardOnEmptyOpenDeck(CardContainer deck)
        {
            var deckChildren = deck.Children.Cast<Panel>();
            var deckSubContainer = deckChildren.First(c => c.GetType() == typeof(CardContainer));
            var subContainerCard = deckSubContainer.Children.Cast<Panel>().First(c => c.GetType() == typeof(Card));

            if ((subContainerCard == null && _moveableContainer != null && !_moveableContainer.Card.Name.Contains("king"))) return false;
            if (deck.Card == null && _moveableCard != null && !(Enum.GetName(typeof(CardName), _moveableCard.CardName.Value)).Contains("king")) return false;
            return true;
        }

        private void SetCardVisualization(CardContainer deck, Card card, bool isOver)
        {
            if (card == null) throw new NullReferenceException();
            if (card.Path == Properties.Resources.EmptyCardPath || card.Path == string.Empty)
            {
                deck.Background = isOver ? Brushes.Blue : null;
            }
            else card.Opacity = isOver ? 0.7 : 1;
        }

        public void TakeCard(object sender, MouseButtonEventArgs args)
        {
            if (_isDrag) return;

            var cardBase = args.Source as Card;

            if (cardBase.CardValue == 0) return;

            if (cardBase.Path == Properties.Resources.BackCardPath)
            {
                _gameViewModel.SetFllipedCardBinding(cardBase.CurrentDeck, cardBase.CardName.Value, cardBase.CardShape.Value);
                return;
            }

            var deckContainer = _openDecks.Find(d => d.ContainerName == cardBase.CurrentDeck);
            FindCardContainer(deckContainer, cardBase);

            if (_moveableContainer != null) 
            {
                var fllipedCardCount = deckContainer.ContainersSource.Count(c => c.CardPath != Properties.Resources.BackCardPath && c.CardPath != Properties.Resources.EmptyCardPath);
                if (fllipedCardCount > 1)
                {
                    SetMoveableContainer(args);
                    return;
                }
            }

            _moveableCard = new Card(cardBase);
            _moveableCard.MouseLeftButtonDown += DropMoveableCard;

            _gameViewModel.SetMoveableCardBinding(_moveableCard.CardName.Value, _moveableCard.CardShape.Value, _moveableCard.CardValue, _moveableCard.CurrentDeck, _moveableCard.Path);

            _isDrag = true;

            SetMoveableItemPosition(args);
            _mainCanvas.Children.Add(_moveableCard);

            _currentDrag = new DropData(_moveableCard.CurrentDeck, _moveableCard);
        }

        private void SetMoveableItemPosition(MouseEventArgs args)
        {
            FrameworkElement moveable = null;
            if (_moveableCard != null)
            {
                moveable = _moveableCard;
                Canvas.SetLeft(moveable, args.GetPosition(_mainCanvas).X - moveable.ActualWidth / 2);
                Canvas.SetTop(moveable, args.GetPosition(_mainCanvas).Y - moveable.ActualHeight / 2);
                return;
            }
            else moveable = _moveableContainer;

            Canvas.SetLeft(moveable, args.GetPosition(_mainCanvas).X - 60);
            Canvas.SetTop(moveable, args.GetPosition(_mainCanvas).Y - 100);
        }

        ContainerViewModel _lastContainer;
        CardContainer _parentContainer;
        List<ContainerViewModel> _containers;
        private void SetMoveableContainer(MouseEventArgs args) 
        {
            _moveableContainer.MouseLeftButtonDown += DropMoveableContainer;

            _containers = new List<ContainerViewModel>();
            _lastContainer = new ContainerViewModel();
            SetContainersList(_moveableContainer);
            _gameViewModel.SetMoveableContainerBinding(_containers);

            _isDrag = true;

            SetMoveableItemPosition(args);
            _parentContainer.Children.Remove(_moveableContainer);
            _parentContainer.SubContainer = null;

            _mainCanvas.Children.Add(_moveableContainer);

            _currentDrag = new DropData(_moveableContainer.ContainerName, _moveableContainer.Card);
        }

        private void FindCardContainer(CardContainer container, Card card)
        {
            if (container == null) return;

            if (container.SubContainer != null)
            {
                if (container.Card != null)
                {
                    if (container.Card.CardName == card.CardName && container.Card.CardShape == card.CardShape)
                    {
                        if (container.SubContainer == null) return;
                        _moveableContainer = container;
                        return;
                    }
                }

                _parentContainer = container;
                FindCardContainer(container.SubContainer, card);
            }
        }

        private void SetContainersList(CardContainer container)
        {
            var containerViewModel = new ContainerViewModel();
            if (container.Card != null)
            {
                if (container.Card.Path != Properties.Resources.BackCardPath)
                {
                    containerViewModel.CardName = container.Card.CardName;
                    containerViewModel.CardShape = container.Card.CardShape;
                    containerViewModel.CardValue = container.Card.CardValue;
                    containerViewModel.DeckName = container.Card.CurrentDeck;
                    containerViewModel.CardPath = container.Card.Path;

                    _containers.Add(containerViewModel);

                    _lastContainer = containerViewModel;
                }
                else
                {
                    _parentContainer = container;
                }
            }

            if (container.SubContainer != null)
            {
                SetContainersList(container.SubContainer);

                var currentIndex = _containers.IndexOf(containerViewModel);
                if (containerViewModel != _lastContainer && currentIndex >= 0)
                {
                    containerViewModel.SubContainer = _containers.ElementAt(currentIndex + 1);
                }
            }
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
