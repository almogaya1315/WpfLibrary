using Solitare.UI.Controls.Image;
using Solitare.UI.Enums;
using Solitare.UI.Game.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Solitare.UI.Game.Views
{
    public partial class GameView : UserControl
    {
        private GameViewModel _gameViewModel;
        private Canvas _mainCanvas;
        private bool _isDrag;
        private Card _moveableCard;

        public GameView()
        {
            InitializeComponent();

            _mainCanvas = MainCanvas;

            AddHandler(MouseMoveEvent, new MouseEventHandler(OnMouseMoveChanged));
        }

        private void OnMouseMoveChanged(Object sender, MouseEventArgs args)
        {
            if (_isDrag)
            {
                Canvas.SetLeft(_moveableCard, args.GetPosition(_mainCanvas).X);
                Canvas.SetTop(_moveableCard, args.GetPosition(_mainCanvas).Y);
            }
        }

        private void InitializeGameViewModel()
        {
            if (_gameViewModel == null) _gameViewModel = new GameViewModel();
        }

        public void DragCard(object sender, MouseButtonEventArgs args)
        {
            InitializeGameViewModel();

            var cardBase = args.Source as Card;
            var cardCanvas = (Canvas)cardBase.Parent;

            var moveableCard = new Card(cardBase);
            _moveableCard = moveableCard;
            var cardViewModel = _gameViewModel.GetCardBehindCurrent(cardBase.CardName, cardBase.CardShape, cardBase.CurrentDeck);
            cardBase = SetCard(cardBase, cardViewModel);

            _isDrag = true;

            Canvas.SetLeft(moveableCard, args.GetPosition(cardBase).X);
            Canvas.SetTop(moveableCard, args.GetPosition(cardBase).Y);
            _mainCanvas.Children.Add(moveableCard);

            var dropData = new DropData(cardBase.CurrentDeck, cardBase);

            var dataObject = new DataObject(typeof(DropData), dropData);

            DragDrop.DoDragDrop(cardBase, dataObject, DragDropEffects.Move);
        }

        private Card SetCard(Card card, CardViewModel cardViewModel)
        {
            card.Path = cardViewModel.Path;
            card.Path = cardViewModel.Path;
            card.Path = cardViewModel.Path;
            card.Path = cardViewModel.Path;
            return card;
        }

        private void DropCard(object sender, DragEventArgs args)
        {
            var canvas = (Canvas)sender;

            var dropData = (DropData)args.Data.GetData(typeof(DropData));

            //Image image = new Image() { Width = imageSource.Width, Height = imageSource.Height, Source = imageSource };

            //(canvas.Children[0] as Image).Source = imageSource;

            //Canvas.SetLeft(image, e.GetPosition(canvas).X);

            //Canvas.SetTop(image, e.GetPosition(canvas).Y);

            //canvas.Children.Add(image);

            InitializeGameViewModel();

            var targetDeck = Enum.GetNames(typeof(DeckName)).Cast<DeckName>().First(e => e.ToString() == canvas.Name);
            _gameViewModel.MoveCard(dropData.SourceDeck, targetDeck, dropData.MovedCard.CardName, dropData.MovedCard.CardShape);
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
