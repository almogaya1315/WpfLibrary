
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace Editor.Controls.DataGrid
{
    public class MyDataGrid : System.Windows.Controls.DataGrid
    {
        public MyDataGrid()
        {
            CanUserAddRows = false;
            CanUserDeleteRows = false;
            CanUserReorderColumns = false;
            CanUserResizeColumns = false;
            CanUserSortColumns = false;
            CanSelectMultipleItems = false;
            AutoGenerateColumns = false;

            AddHandler(GotFocusEvent, new RoutedEventHandler(OnFocusEditCell));
        }

        private void OnFocusEditCell(object sender, RoutedEventArgs args)
        {

        }

        public List<ColumnDefinition> ColumnsBindings
        {
            get { return (List<ColumnDefinition>)GetValue(ColumnsBindingsProperty); }
            set { SetValue(ColumnsBindingsProperty, value); }
        }

        public static readonly DependencyProperty ColumnsBindingsProperty =
            DependencyProperty.Register("ColumnsBindings",
                typeof(List<ColumnDefinition>), typeof(MyDataGrid),
                new PropertyMetadata(null, ColumnsBindingsChanged));

        private static void ColumnsBindingsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = (MyDataGrid)d;
            if (grid == null) return;

            var columns = (List<ColumnDefinition>)d.GetValue(ColumnsBindingsProperty);
            if (columns == null || columns.Count == 0) return;

            foreach (var c in columns)
            {
                var column = new MyTemplateColumn()
                {
                    Header = c.Header,
                    DataContextPath = c.DataContextPath,
                    CellTemplate = (DataTemplate)grid.FindResource(c.DataTemplateName),
                };

                if (c.EditingDataTemplateName != null)
                    column.CellEditingTemplate = (DataTemplate)grid.FindResource(c.EditingDataTemplateName);
                else column.IsReadOnly = true;

                grid.Columns.Add(column);
            }
        }
    }
}
