using Solitare.UI.Controls.Image;
using Solitare.UI.Enums;
using Solitare.UI.Game.ViewModels;
using System;
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

        private Canvas _mainCanvas;
        private Card _moveableCard;

        public GameView()
        {
            InitializeComponent();
            DataContext = new GameViewModel();
            _gameViewModel = (GameViewModel)DataContext;

            _mainCanvas = MainCanvas;

            AddHandler(MouseMoveEvent, new MouseEventHandler(OnMouseMoveChanged));
        }

        private void MoveableCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            if (_moveableCard.IsOverDiamondsDeck)
            {
                _gameViewModel.DropCard(_currentDrag.SourceDeck, DeckName.DiamondsDeckCard, _currentDrag.MovedCard.CardName, _currentDrag.MovedCard.CardShape, _currentDrag.MovedCard.Path);

                DiamondsDeckCard.Background = null;
                _mainCanvas.Children.Remove(_moveableCard);
                _moveableCard = null;
                _isDrag = false;
            }
        }

        private void OnMouseMoveChanged(object sender, MouseEventArgs args)
        {
            if (_isDrag)
            {
                SetCardPosition(args);

                var DiamondsDeckCardPoint = DiamondsDeckCard.TransformToAncestor(Application.Current.MainWindow)
                                                            .Transform(new Point(0, 0));

                if (args.GetPosition(_mainCanvas).X >= DiamondsDeckCardPoint.X - 50 && args.GetPosition(_mainCanvas).X <= DiamondsDeckCardPoint.X + DiamondsDeckCard.ActualWidth + 50 &&
                    args.GetPosition(_mainCanvas).Y >= DiamondsDeckCardPoint.Y - 70 && args.GetPosition(_mainCanvas).Y <= DiamondsDeckCardPoint.Y + DiamondsDeckCard.ActualHeight + 70)
                {
                    // TODO: deck match validation

                    DiamondsDeckCard.Background = Brushes.Red;

                    _moveableCard.IsOverDiamondsDeck = true;
                }
                else
                {
                    DiamondsDeckCard.Background = null;

                    _moveableCard.IsOverDiamondsDeck = false;
                }
            }
        }

        public void TakeCard(object sender, MouseButtonEventArgs args)
        {
            var cardBase = args.Source as Card;

            _moveableCard = new Card(cardBase);
            _moveableCard.MouseLeftButtonDown += MoveableCard_MouseLeftButtonDown;

            _gameViewModel.SetMoveableCardBinding(_moveableCard.CardName, _moveableCard.CardShape, _moveableCard.CurrentDeck);

            _isDrag = true;

            SetCardPosition(args);
            _mainCanvas.Children.Add(_moveableCard);

            _currentDrag = new DropData(_moveableCard.CurrentDeck, _moveableCard);

            //var dropObject = new DataObject(typeof(DropData), dropData);
            //DragDrop.DoDragDrop(cardBase, dataObject, DragDropEffects.Move);
        }

        private void SetCardPosition(MouseEventArgs args)
        {
            Canvas.SetLeft(_moveableCard, args.GetPosition(_mainCanvas).X - _moveableCard.ActualWidth / 2);
            Canvas.SetTop(_moveableCard, args.GetPosition(_mainCanvas).Y - _moveableCard.ActualHeight / 2);
        }

        //private void DropCard(Canvas canvas)
        //{
        //    //var dropData = (DropData)args.Data.GetData(typeof(DropData));

        //    //Image image = new Image() { Width = imageSource.Width, Height = imageSource.Height, Source = imageSource };

        //    //(canvas.Children[0] as Image).Source = imageSource;

        //    //Canvas.SetLeft(image, e.GetPosition(canvas).X);

        //    //Canvas.SetTop(image, e.GetPosition(canvas).Y);

        //    //canvas.Children.Add(image);

        //    var targetDeck = Enum.GetNames(typeof(DeckName)).Cast<DeckName>().First(e => e.ToString() == canvas.Name);
        //    _gameViewModel.MoveCard(_currentDrag.SourceDeck, targetDeck, _currentDrag.MovedCard.CardName, _currentDrag.MovedCard.CardShape);
        //}
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
