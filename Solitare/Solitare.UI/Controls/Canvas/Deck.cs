using Solitare.UI.Controls.Image;
using Solitare.UI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Solitare.UI.Controls.Canvas
{
    public class Deck : System.Windows.Controls.Canvas
    {
        private static MouseButtonEventHandler _takeCardEventHandler;

        public static readonly DependencyProperty DeckNameProperty =
            DependencyProperty.Register("DeckName", typeof(DeckName), typeof(Deck), new PropertyMetadata(null));

        public static readonly DependencyProperty IsDraggableProperty =
            DependencyProperty.Register("IsDraggable", 
                typeof(bool), typeof(Deck), 
                new PropertyMetadata(false, OnIsDraggablePropertyChanged));

        public static readonly DependencyProperty TakeCardEventResourceProperty =
            DependencyProperty.Register("TakeCardEventResource",
                typeof(EventResource), typeof(Deck),
                new PropertyMetadata(null, OnTakeCardEventResourcePropertyChanged));

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

        public EventResource TakeCardEventResource
        {
            get { return (EventResource)GetValue(TakeCardEventResourceProperty); }
            set { SetValue(TakeCardEventResourceProperty, value); }
        }

        public event MouseButtonEventHandler TakeCardEvent
        {
            add
            {
                _takeCardEventHandler += value;
            }
            remove
            {
                _takeCardEventHandler -= value;
            }
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

        private static void OnTakeCardEventResourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var deck = (Deck)d;
            if (deck == null) return;

            var resource = (EventResource)d.GetValue(TakeCardEventResourceProperty);
            if (resource == null) return;

            var style = new Style(resource.TargetType);
            var setter = new EventSetter(resource.Event, _takeCardEventHandler);
            style.Setters.Add(setter);
            deck.Resources.Add(resource.TargetType, style);
        }
    }
}
