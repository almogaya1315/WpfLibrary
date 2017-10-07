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

namespace Solitare.UI.Game.Views
{
    public partial class GameView : UserControl
    {
        private GameViewModel _gameViewModel;

        private DropData _currentDrag;
        private bool _isDrag;

        private List<Canvas> _closedDecks;
        private Canvas _mainCanvas;
        private Card _moveableCard;

        public GameView()
        {
            InitializeComponent();
            DataContext = new GameViewModel();
            _gameViewModel = (GameViewModel)DataContext;

            _mainCanvas = MainCanvas;
            _closedDecks = new List<Canvas>() { OpenDeckCard, DiamondsDeckCard, ClubsDeckCard, HeartsDeckCard, SpadesDeckCard };

            AddHandler(MouseMoveEvent, new MouseEventHandler(OnMouseMoveChanged));
        }

        private void MoveableCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            if (_moveableCard.isOverOpenDeck)
            {
                DropCard(OpenDeckCard, DeckName.OpenDeckCard);
            }
            else if (_moveableCard.IsOverDiamondsDeck)
            {
                DropCard(DiamondsDeckCard, DeckName.DiamondsDeckCard);
            }
            else if (_moveableCard.IsOverClubsDeck)
            {
                DropCard(ClubsDeckCard, DeckName.ClubsDeckCard);
            }
            else if (_moveableCard.IsOverHeartsDeck)
            {
                DropCard(HeartsDeckCard, DeckName.HeartsDeckCard);
            }
            else if (_moveableCard.IsOverSpadesDeck)
            {
                DropCard(SpadesDeckCard, DeckName.SpadesDeckCard);
            }
        }

        private void DropCard(Canvas deck, DeckName deckName)
        {
            _gameViewModel.DropCard(_currentDrag.SourceDeck, deckName, _currentDrag.MovedCard.CardName, _currentDrag.MovedCard.CardShape, _currentDrag.MovedCard.Path);

            deck.Background = null;
            _mainCanvas.Children.Remove(_moveableCard);
            _moveableCard = null;
            _isDrag = false;
        }

        private void OnMouseMoveChanged(object sender, MouseEventArgs args)
        {
            if (_isDrag)
            {
                SetMoveableCardPosition(args);

                _closedDecks.ForEach(d => FindOverDeck(d, args));
            }
        }

        private void FindOverDeck(Canvas deck, MouseEventArgs args)
        {
            var point = deck.TransformToAncestor(Application.Current.MainWindow)
                                                            .Transform(new Point(0, 0));

            if (args.GetPosition(_mainCanvas).X >= point.X - 50 && args.GetPosition(_mainCanvas).X <= point.X + deck.ActualWidth + 50 &&
                args.GetPosition(_mainCanvas).Y >= point.Y - 70 && args.GetPosition(_mainCanvas).Y <= point.Y + deck.ActualHeight + 70)
            {
                // TODO: deck match validation

                deck.Background = Brushes.Red;

                SetIsMouseOver(deck, true);
            }
            else
            {
                deck.Background = null;

                SetIsMouseOver(deck, false);
            }
        }

        private void SetIsMouseOver(Canvas deck, bool value)
        {
            switch (deck.Name)
            {
                case "OpenDeckCard":
                    _moveableCard.isOverOpenDeck = value;
                    break;
                case "DiamondsDeckCard":
                    _moveableCard.IsOverDiamondsDeck = value;
                    break;
                case "SpadesDeckCard":
                    _moveableCard.IsOverSpadesDeck = value;
                    break;
                case "HeartsDeckCard":
                    _moveableCard.IsOverHeartsDeck = value;
                    break;
                case "ClubsDeckCard":
                    _moveableCard.IsOverClubsDeck = value;
                    break;
                default:
                    break;
            }
        }

        public void TakeCard(object sender, MouseButtonEventArgs args)
        {
            var cardBase = args.Source as Card;

            _moveableCard = new Card(cardBase);
            _moveableCard.MouseLeftButtonDown += MoveableCard_MouseLeftButtonDown;

            _gameViewModel.SetMoveableCardBinding(_moveableCard.CardName, _moveableCard.CardShape, _moveableCard.CurrentDeck);

            _isDrag = true;

            SetMoveableCardPosition(args);
            _mainCanvas.Children.Add(_moveableCard);

            _currentDrag = new DropData(_moveableCard.CurrentDeck, _moveableCard);

            //var dropObject = new DataObject(typeof(DropData), dropData);
            //DragDrop.DoDragDrop(cardBase, dataObject, DragDropEffects.Move);
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
