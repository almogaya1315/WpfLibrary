using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Editor.Controls.DataGrid
{
    public class MyTemplateColumn : DataGridTemplateColumn
    {
        public string DataContextPath { get; set; }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            var contentPresenter = base.GenerateElement(cell, dataItem);

            BindingOperations.SetBinding(contentPresenter, ContentPresenter.ContentProperty, new Binding(DataContextPath));

            return contentPresenter;
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            var contentPresenter = base.GenerateEditingElement(cell, dataItem);

            BindingOperations.SetBinding(contentPresenter, ContentPresenter.ContentProperty, new Binding(DataContextPath));

            return contentPresenter;
        }
    }
}
