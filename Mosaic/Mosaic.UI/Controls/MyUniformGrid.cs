using Mosaic.UI.Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Mosaic.UI.Controls
{
    public class MyUniformGrid : UniformGrid
    {
        public List<CardViewModel> ItemsSourceBindings
        {
            get { return (List<CardViewModel>)GetValue(ItemsSourceBindingsProperty); }
            set { SetValue(ItemsSourceBindingsProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceBindingsProperty =
            DependencyProperty.Register("ItemsSourceBindings", 
                typeof(List<CardViewModel>), typeof(MyUniformGrid), 
                new PropertyMetadata(null, ItemsSourceBindingsPropertyChanged));

        private static void ItemsSourceBindingsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (MyUniformGrid)d;
            if (control == null) return;

            var cards = (List<CardViewModel>)d.GetValue(ItemsSourceBindingsProperty);
            if (cards.Count == 0) return;

            foreach (var card in cards)
            {
                //control.Children.Add(new  //Items.Add(new MyControlColumn()
                //{
                //    //CellTemplate = (DataTemplate)control.FindResource(card.TemplateName),
                //});
            }
        }
    }
}
