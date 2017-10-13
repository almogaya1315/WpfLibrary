using Solitare.UI.Controls.Canvas;
using Solitare.UI.Controls.Image;
using Solitare.UI.Enums;
using Solitare.UI.Game.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        private void DropCard(CardContainer deck)
        {
            _gameViewModel.DropCard(deck.ContainerName);

            deck.Background = null;
            _mainCanvas.Children.Remove(_moveableCard);
            _moveableCard = null;
            _isDrag = false;
        }

        private void FindOverDeck(CardContainer deck, MouseEventArgs args)
        {
            var point = deck.TransformToAncestor(Application.Current.MainWindow)
                                                            .Transform(new Point(0, 0));

            if (args.GetPosition(_mainCanvas).X >= point.X && args.GetPosition(_mainCanvas).X <= point.X + deck.ActualWidth &&
                args.GetPosition(_mainCanvas).Y >= point.Y - 70 && args.GetPosition(_mainCanvas).Y <= point.Y + deck.ActualHeight + 70)
            {
                if (_gameViewModel.ValidateDeck(deck.Name) == DeckMatch.NotFound) return;

                SetIsMouseOver(deck, true);
            }
            else
            {
                SetIsMouseOver(deck, false);
            }
        }

        private void FindOverCard(CardContainer deck, MouseEventArgs args)
        {
            var point = deck.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));

            if (args.GetPosition(_mainCanvas).X >= point.X && args.GetPosition(_mainCanvas).X <= point.X + deck.ActualWidth &&
                args.GetPosition(_mainCanvas).Y >= point.Y - 70 && args.GetPosition(_mainCanvas).Y <= point.Y + deck.ActualHeight + 70)
            {
                //if (_gameViewModel.ValidateCard(deck.Children.))
            }
        }

        private void SetIsMouseOver(CardContainer deck, bool isOver)
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
                default:
                    break;
            }
        }

        public void TakeCard(object sender, MouseButtonEventArgs args)
        {
            var cardBase = args.Source as Card;

            foreach (var child in _mainCanvas.Children)
            {
                if (child is Card)
                {
                    var card = (Card)child;
                    if (card.Name == cardBase.Name && card.CardShape == cardBase.CardShape) return;
                }
            }

            if (cardBase.CardValue == 0) return;

            if (cardBase.Path == Properties.Resources.BackCardPath)
            {
                _gameViewModel.SetFllipedCardBinding(cardBase.CurrentDeck, cardBase.CardName, cardBase.CardShape);
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
            Canvas.SetLeft(_moveableCard, args.GetPosition(_mainCanvas).X - _moveableCard.ActualWidth / 2);
            Canvas.SetTop(_moveableCard, args.GetPosition(_mainCanvas).Y - _moveableCard.ActualHeight / 2);
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
