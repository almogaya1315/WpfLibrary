using Solitare.UI.Controls.Image;
using Solitare.UI.Enums;
using Solitare.UI.Game.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Solitare.UI.Controls.Canvas
{
    public class CardContainer : System.Windows.Controls.Canvas
    {
        public static MouseButtonEventHandler TakeCardEventHandler;

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

        public static readonly DependencyProperty ContainersSourceProperty =
            DependencyProperty.Register("ContainersSource",
                typeof(List<ContainerViewModel>), typeof(CardContainer),
                new PropertyMetadata(null, OnContainersSourcePropertyChanged));

        public CardContainer SubContainer { get; set; }
        public Card Card { get; set; }

        public bool IsOverFirstDeck { get; set; }
        public bool IsOverSecondDeck { get; set; }
        public bool IsOverThirdDeck { get; set; }
        public bool IsOverFourthDeck { get; set; }
        public bool IsOverFifthDeck { get; set; }
        public bool IsOverSixthDeck { get; set; }
        public bool IsOverSeventhDeck { get; set; }

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

        public List<ContainerViewModel> ContainersSource
        {
            get { return (List<ContainerViewModel>)GetValue(ContainersSourceProperty); }
            set { SetValue(ContainersSourceProperty, value); }
        }

        public event MouseButtonEventHandler TakeCardEvent
        {
            add
            {
                TakeCardEventHandler += value;
            }
            remove
            {
                TakeCardEventHandler -= value;
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
            var setter = new EventSetter(resource.Event, TakeCardEventHandler);
            style.Setters.Add(setter);
            deck.Resources.Add(resource.TargetType, style);
        }

        private static void OnContainersSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var deck = (CardContainer)d;
            if (deck == null) return;

            var containersSource = (List<ContainerViewModel>)d.GetValue(ContainersSourceProperty);
            var firstContainer = containersSource.FirstOrDefault();
            deck.Children.Clear();

            if (firstContainer == null)
            {
                SetEmptyDeck(deck);
                return;
            }
            
            SetDeckCards(containersSource, deck, firstContainer);
        }

        private static void SetEmptyDeck(CardContainer baseContainer)
        {
            var cardContainer = new CardContainer();
            cardContainer.ContainerName = baseContainer.ContainerName;

            cardContainer.Children.Add(new Card()
            {
                Source = new BitmapImage(new Uri(Properties.Resources.EmptyCardPath, UriKind.Relative)),
                Path = Properties.Resources.EmptyCardPath,
                FrontCardPath = string.Empty,
                CardName = CardName.Empty,
                CardShape = CardShape.Empty,
                CardValue = 0,
                CurrentDeck = baseContainer.ContainerName,
                Margin = new Thickness(14, 5, 14, 5),
                Height = 149,
            });

            SetZIndex(cardContainer, 0);

            cardContainer.SetValue(IsDraggableProperty, false);

            baseContainer.Children.Add(cardContainer);
            baseContainer.SubContainer = cardContainer;
        }

        private static void SetDeckCards(List<ContainerViewModel> containersSource, CardContainer baseContainer, ContainerViewModel subContainer)
        {
            var cardContainer = new CardContainer();
            cardContainer.ContainerName = baseContainer.ContainerName;

            var card = new Card()
            {
                Source = new BitmapImage(new Uri(subContainer.CardPath, UriKind.Relative)),
                Path = subContainer.CardPath,
                FrontCardPath = subContainer.FrontCardPath,
                CardName = subContainer.CardName.Value,
                CardShape = subContainer.CardShape.Value,
                CardValue = subContainer.CardValue,
                CurrentDeck = subContainer.DeckName,
                Margin = new Thickness(14, 5, 14, 5),
                Height = 149,
            };
            cardContainer.Children.Add(card);
            cardContainer.Card = card;

            cardContainer.SetValue(TakeCardEventResourceProperty, subContainer.TakeCardEventResource);

            var zIndex = containersSource.IndexOf(subContainer) + 1;
            SetZIndex(cardContainer, zIndex);

            if (subContainer.CardPath == Properties.Resources.BackCardPath)
            {
                if (zIndex > 1) cardContainer.Margin = new Thickness(0, 10, 0, 0);
                cardContainer.SetValue(IsDraggableProperty, false);
            }
            else if (containersSource.Count == 1)
            {
                cardContainer.SetValue(IsDraggableProperty, true);
            }
            else
            {
                cardContainer.Margin = new Thickness(0, 30, 0, 0);
                cardContainer.SetValue(IsDraggableProperty, true);
            }

            baseContainer.Children.Add(cardContainer);
            baseContainer.SubContainer = cardContainer;

            if (containersSource.ElementAtOrDefault(zIndex) != null)
            {
                subContainer.SubContainer = containersSource.ElementAt(zIndex);
                SetDeckCards(containersSource, cardContainer, subContainer.SubContainer);
            }
        }
    }
}
