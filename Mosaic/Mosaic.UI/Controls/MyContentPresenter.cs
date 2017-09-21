using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Mosaic.UI.Controls
{
    public class MyContentPresenter : ContentPresenter
    {
        public MyContentPresenter()
        {
            BindingOperations.SetBinding(this, ContentPresenter.ContentProperty, new Binding("CardViewModel"));
        }
    }
}
