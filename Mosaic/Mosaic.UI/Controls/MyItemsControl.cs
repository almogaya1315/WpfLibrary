using Mosaic.UI.Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Mosaic.UI.Controls
{
    public class MyItemsControl : ItemsControl
    {
        public List<CardViewModel> ItemsSourceBindings
        {
            get { return (List<CardViewModel>)GetValue(ItemsSourceBindingsProperty); }
            set { SetValue(ItemsSourceBindingsProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceBindingsProperty =
            DependencyProperty.Register("ItemsSourceBindings", 
                typeof(List<CardViewModel>), typeof(MyItemsControl), 
                new PropertyMetadata(null, ItemsSourceBindingsPropertyChanged));

        private static void ItemsSourceBindingsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = (MyItemsControl)d;
            if (grid == null) return;

            var cards = (List<CardViewModel>)d.GetValue(ItemsSourceBindingsProperty);
            if (cards.Count == 0) return;

            grid.Items.Clear();
            foreach (var card in cards)
            {
                if (card.TemplateName == "MoveableCardTemplate")
                {

                }

                var contentPresenter = new ContentPresenter()
                {
                    ContentTemplate = (DataTemplate)grid.FindResource(card.TemplateName),
                };

                var binding = new Binding()
                {
                    Source = card,
                    Path = new PropertyPath(card.DataContextPath),
                    Mode = BindingMode.OneWay
                };
                var contentBinding = BindingOperations.SetBinding(contentPresenter, ContentPresenter.ContentProperty, binding);

                grid.Items.Add(contentPresenter);
            }
        }
    }
}
