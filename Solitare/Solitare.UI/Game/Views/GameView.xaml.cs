﻿using Solitare.UI.Controls.Canvas;
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

        private List<Deck> _closedDecks;
        private Canvas _mainCanvas;
        private Card _moveableCard;

        public GameView()
        {
            InitializeComponent();
            DataContext = new GameViewModel();
            _gameViewModel = (GameViewModel)DataContext;

            _mainCanvas = MainCanvas;
            _closedDecks = new List<Deck>() { OpenDeckCard, DiamondsDeckCard, ClubsDeckCard, HeartsDeckCard, SpadesDeckCard };

            AddHandler(MouseMoveEvent, new MouseEventHandler(OnMouseMoveChanged));
        }

        private void MoveableCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            if (_moveableCard.isOverOpenDeck) DropCard(OpenDeckCard);
            else if (_moveableCard.IsOverDiamondsDeck) DropCard(DiamondsDeckCard);
            else if (_moveableCard.IsOverClubsDeck) DropCard(ClubsDeckCard);
            else if (_moveableCard.IsOverHeartsDeck) DropCard(HeartsDeckCard);
            else if (_moveableCard.IsOverSpadesDeck) DropCard(SpadesDeckCard);
        }

        private void DropCard(Deck deck)
        {
            _gameViewModel.DropCard(deck.DeckName);

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

        private void FindOverDeck(Deck deck, MouseEventArgs args)
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

        private void SetIsMouseOver(Deck deck, bool isOver)
        {
            switch (deck.DeckName)
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

            if (cardBase.CardValue == 0) return;

            _moveableCard = new Card(cardBase);
            _moveableCard.MouseLeftButtonDown += MoveableCard_MouseLeftButtonDown;

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
