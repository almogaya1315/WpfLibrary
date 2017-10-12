using Solitare.UI.Controls.Image;
using Solitare.UI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Solitare.UI.Controls.Canvas
{
    public class Deck : System.Windows.Controls.Canvas
    {
        public static readonly DependencyProperty DeckNameProperty =
            DependencyProperty.Register("DeckName", typeof(DeckName), typeof(Deck), new PropertyMetadata(null));

        public static readonly DependencyProperty IsDraggableProperty =
            DependencyProperty.Register("IsDraggable", 
                typeof(bool), typeof(Deck), 
                new PropertyMetadata(false, OnIsDraggablePropertyChanged));

        public static readonly DependencyProperty TakeCardEventBindingProperty =
            DependencyProperty.Register("TakeCardEventBinding",
                typeof(ResourceDictionary), typeof(Deck),
                new PropertyMetadata(null, OnTakeCardEventBindingPropertyChanged));

        public static readonly DependencyProperty TakeCardEventHandlerProperty =
            DependencyProperty.Register("TakeCardEventHandler",
                typeof(Delegate), typeof(Deck),
                new PropertyMetadata(null, OnTakeCardEventHandlerPropertyChanged));

        public DeckName DeckName
        {
            get { return (DeckName)GetValue(DeckNameProperty); }
            set { SetValue(DeckNameProperty, value); }
        }

        public bool IsDraggable
        {
            get { return (bool)GetValue(IsDraggableProperty); }
            set { SetValue(IsDraggableProperty, value); }
        }

        public ResourceDictionary TakeCardEventBinding
        {
            get { return (ResourceDictionary)GetValue(TakeCardEventBindingProperty); }
            set { SetValue(TakeCardEventBindingProperty, value); }
        }

        public Delegate TakeCardEventHandler
        {
            get { return (Delegate)GetValue(TakeCardEventHandlerProperty); }
            set { SetValue(TakeCardEventHandlerProperty, value); }
        }

        private static void OnIsDraggablePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var deck = (Deck)d;
            if (deck == null) return;

            var isDraggable = (bool)d.GetValue(IsDraggableProperty);

            if (isDraggable)
            {

            }
        }

        private static void OnTakeCardEventBindingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var deck = (Deck)d;
            if (deck == null) return;

            var resource = d.GetValue(TakeCardEventBindingProperty);

            //deck.Resources = (ResourceDictionary)d.GetValue(IsDraggableProperty);

            //TakeCardEventResource.Add(new Style(typeof(Card)), new EventSetter(MouseLeftButtonDownEvent, TakeCard));
        }

        private static void OnTakeCardEventHandlerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var deck = (Deck)d;
            if (deck == null) return;

            var method = d.GetValue(TakeCardEventHandlerProperty);
        }
    }
}
