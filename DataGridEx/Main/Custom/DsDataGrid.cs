using Main.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Main.Custom
{
    public class DsDataGrid : DataGrid
    {
        public List<ColumnViewModel> ViewModelColumns
        {
            get { return (List<ColumnViewModel>)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("ViewModelColumns", 
                typeof(List<ColumnViewModel>), typeof(DsDataGrid), 
                new PropertyMetadata(null, ColumnsChanged));

        static void ColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = (DsDataGrid)d;
            if (grid == null) return;

            var columns = (List<ColumnViewModel>)d.GetValue(ColumnsProperty);
            if (columns == null || columns.Count == 0) return;

            foreach (var c in columns)
            {
                grid.Columns.Add(new DsDataGridColumn()
                {
                    Header = c.Header,
                    DataContext = c.DataContext,
                    CellTemplate = (DataTemplate)grid.FindResource(c.DataTemplate),
                    CellEditingTemplate = (DataTemplate)grid.FindResource(c.EditingDataTemplate)
                });
            }
        }
    }
}
