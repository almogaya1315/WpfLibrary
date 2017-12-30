using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            DataGridCell cell = null;
            TextBlock box = null;
            
            if (e.OriginalSource is DataGridCell)
            {
                cell = (DataGridCell)e.OriginalSource;
                cell.IsEditing = true;

                box = XamlHelper.GetChildOfType<TextBlock>(cell);
                box.Focus();
            }
            //else if (e.OriginalSource is DsTextBox)
            //{
            //    box = (DsTextBox)e.OriginalSource;
            //    box.Focus();
            //}
        }
    }
}
