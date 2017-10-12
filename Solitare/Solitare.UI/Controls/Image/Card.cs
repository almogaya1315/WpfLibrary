using Solitare.UI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Solitare.UI.Controls.Image
{
    public class Card : System.Windows.Controls.Image
    {
        public bool isOverOpenDeck { get; set; }
        public bool IsOverDiamondsDeck { get; set; }
        public bool IsOverClubsDeck { get; set; }
        public bool IsOverHeartsDeck { get; set; }
        public bool IsOverSpadesDeck { get; set; }

        public string FrontCardPath { get; set; }

        public Card()
        {

        }

        public Card(Card card)
        {
            Path = card.Path;
            CardName = card.CardName;
            CardShape = card.CardShape;
            CardValue = card.CardValue;
            CurrentDeck = card.CurrentDeck;
            Source = card.Source;
            Height = 149;
        }

        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }
        public CardName CardName
        {
            get { return (CardName)GetValue(CardNameProperty); }
            set { SetValue(CardNameProperty, value); }
        }
        public CardShape CardShape
        {
            get { return (CardShape)GetValue(CardShapeProperty); }
            set { SetValue(CardShapeProperty, value); }
        }
        public int CardValue
        {
            get { return (int)GetValue(CardValueProperty); }
            set { SetValue(CardValueProperty, value); }
        }
        public DeckName CurrentDeck
        {
            get { return (DeckName)GetValue(CurrentDeckProperty); }
            set { SetValue(CurrentDeckProperty, value); }
        }

        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register("Path", typeof(string), typeof(Card), new PropertyMetadata(""));

        public static readonly DependencyProperty CardNameProperty =
            DependencyProperty.Register("CardName", typeof(CardName), typeof(Card), new PropertyMetadata(null));

        public static readonly DependencyProperty CardShapeProperty =
            DependencyProperty.Register("CardShape", typeof(CardShape), typeof(Card), new PropertyMetadata(null));

        public static readonly DependencyProperty CardValueProperty =
            DependencyProperty.Register("CardValue", typeof(int), typeof(Card), new PropertyMetadata(0));

        public static readonly DependencyProperty CurrentDeckProperty =
            DependencyProperty.Register("CurrentDeck", typeof(DeckName), typeof(Card), new PropertyMetadata(null));
    }
}
