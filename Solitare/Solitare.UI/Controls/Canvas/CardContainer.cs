using Solitare.UI.Controls.Image;
using Solitare.UI.Enums;
using Solitare.UI.Game.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Solitare.UI.Controls.Canvas
{
    public class CardContainer : System.Windows.Controls.Canvas
    {
        private static MouseButtonEventHandler _takeCardEventHandler;

        public static readonly DependencyProperty ContainerNameProperty =
            DependencyProperty.Register("ContainerName", typeof(DeckName), typeof(CardContainer), new PropertyMetadata(null));

        public static readonly DependencyProperty IsDraggableProperty =
            DependencyProperty.Register("IsDraggable",
                typeof(bool), typeof(CardContainer),
                new PropertyMetadata(false, OnIsDraggablePropertyChanged));

        public static readonly DependencyProperty TakeCardEventResourceProperty =
            DependencyProperty.Register("TakeCardEventResource",
                typeof(EventResource), typeof(CardContainer),
                new PropertyMetadata(null, OnTakeCardEventResourcePropertyChanged));

        public static readonly DependencyProperty SubContainerProperty =
            DependencyProperty.Register("SubContainer",
                typeof(ContainerViewModel), typeof(CardContainer),
                new PropertyMetadata(null, OnSubContainerPropertyChanged));

        public DeckName ContainerName
        {
            get { return (DeckName)GetValue(ContainerNameProperty); }
            set { SetValue(ContainerNameProperty, value); }
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

        public List<ContainerViewModel> SubContainersSource
        {
            get { return (List<ContainerViewModel>)GetValue(SubContainerProperty); }
            set { SetValue(SubContainerProperty, value); }
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
            var deck = (CardContainer)d;
            if (deck == null) return;

            var isDraggable = (bool)d.GetValue(IsDraggableProperty);

            if (isDraggable)
            {

            }
        }

        private static void OnTakeCardEventResourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var deck = (CardContainer)d;
            if (deck == null) return;

            var resource = (EventResource)d.GetValue(TakeCardEventResourceProperty);
            if (resource == null) return;

            var style = new Style(resource.TargetType);
            var setter = new EventSetter(resource.Event, _takeCardEventHandler);
            style.Setters.Add(setter);
            deck.Resources.Add(resource.TargetType, style);
        }

        private static void OnSubContainerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var deck = (CardContainer)d;
            if (deck == null) return;

            var subContainer = (ContainerViewModel)d.GetValue(SubContainerProperty);
            if (subContainer == null) return;

            SetDeckCards(d, deck, subContainer);
        }

        private static void SetDeckCards(DependencyObject d, CardContainer baseContainer, ContainerViewModel subContainer)
        {
            var cardContainer = new CardContainer();

            cardContainer.Children.Add(new Card()
            {
                Source = new BitmapImage(new Uri(subContainer.CardPath, UriKind.Relative)),
                Path = subContainer.CardPath,
                CardName = subContainer.CardName,
                CardShape = subContainer.CardShape,
                CardValue = subContainer.CardValue,
                CurrentDeck = (DeckName)d.GetValue(ContainerNameProperty),
                Margin = new Thickness(14, 5, 14, 5),
                Height = 149,

                // zIndex
            });

            if (subContainer.CardPath == Properties.Resources.BackCardPath)
            {
                cardContainer.Height = 20;
                cardContainer.SetValue(IsDraggableProperty, false);
            }
            else
            {
                cardContainer.Height = 50;
                cardContainer.SetValue(IsDraggableProperty, true);
                cardContainer.TakeCardEvent += _takeCardEventHandler;
                cardContainer.SetValue(TakeCardEventResourceProperty, subContainer.TakeCardEventResource);
            }

            baseContainer.Children.Add(cardContainer);

            if (subContainer.SubContainer != null)
            {
                SetDeckCards(d, cardContainer, subContainer.SubContainer);
            }
        }
    }
}
