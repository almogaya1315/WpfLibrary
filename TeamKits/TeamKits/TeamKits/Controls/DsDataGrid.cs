using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TeamKits.Base;

namespace TeamKits.Controls
{
    public class DsDataGrid : System.Windows.Controls.DataGrid
    {
        public DsDataGrid()
        {
            AddHandler(GotFocusEvent, new RoutedEventHandler(OnAnyGotFocus));
        }

        private void OnAnyGotFocus(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource.GetType() != typeof(DataGridCell)) return;
            var cell = (DataGridCell)e.OriginalSource;

            if (cell.IsEditing)
            {
                cell.Focus();
                
            }
        }
    }
}
