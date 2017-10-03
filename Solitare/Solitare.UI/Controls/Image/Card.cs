using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Solitare.UI.Controls.Image
{
    public class Card : System.Windows.Controls.Image
    {
        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register("Path", typeof(string), typeof(Card), new PropertyMetadata(""));


        public string CardName
        {
            get { return (string)GetValue(CardNameProperty); }
            set { SetValue(CardNameProperty, value); }
        }

        public static readonly DependencyProperty CardNameProperty =
            DependencyProperty.Register("CardName", typeof(string), typeof(Card), new PropertyMetadata(""));

        public string CardShape
        {
            get { return (string)GetValue(CardShapeProperty); }
            set { SetValue(CardShapeProperty, value); }
        }

        public static readonly DependencyProperty CardShapeProperty =
            DependencyProperty.Register("CardShape", typeof(string), typeof(Card), new PropertyMetadata(""));


        public string CurrentDeck
        {
            get { return (string)GetValue(CurrentDeckProperty); }
            set { SetValue(CurrentDeckProperty, value); }
        }

        public static readonly DependencyProperty CurrentDeckProperty =
            DependencyProperty.Register("CurrentDeck", typeof(string), typeof(Card), new PropertyMetadata(""));
    }
}
