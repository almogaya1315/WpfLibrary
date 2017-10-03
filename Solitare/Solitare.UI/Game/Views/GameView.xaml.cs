using Solitare.UI.Controls.Image;
using Solitare.UI.Game.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Solitare.UI.Game.Views
{
    public partial class GameView : UserControl
    {
        public GameView()
        {
            InitializeComponent();
        }

        public void DragCard(object sender, MouseButtonEventArgs e)
        {
            var card = e.Source as Card;

            var dropData = new DropData(card.CurrentDeck, card);

            var dataObject = new DataObject(typeof(DropData), dropData);

            DragDrop.DoDragDrop(card, dataObject, DragDropEffects.Move);
        }

        private void DropCard(object sender, DragEventArgs e)
        {
            var canvas = (Canvas)sender;

            var dropData = (DropData)e.Data.GetData(typeof(DropData));

            //Image image = new Image() { Width = imageSource.Width, Height = imageSource.Height, Source = imageSource };

            //(canvas.Children[0] as Image).Source = imageSource;

            //Canvas.SetLeft(image, e.GetPosition(canvas).X);
            
            //Canvas.SetTop(image, e.GetPosition(canvas).Y);

            //canvas.Children.Add(image);

            (DataContext as GameViewModel).MoveCard(dropData.SourceDeck, canvas.Name, dropData.MovedCard.CardName, dropData.MovedCard.CardShape);
        }
    }

    public class DropData
    {
        public DropData(string sourceDeck, Card movedCard)
        {
            SourceDeck = sourceDeck;
            MovedCard = movedCard;
        }

        public string SourceDeck { get; set; }

        public Card MovedCard { get; set; }
    }
}
