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
        private DataGridCell _focusedCall;

        public DsDataGrid()
        {
            AddHandler(GotFocusEvent, new RoutedEventHandler(OnAnyGotFocus));
        }

        private void OnAnyGotFocus(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is DataGridCell)
            {
                _focusedCall = (DataGridCell)e.OriginalSource;
                _focusedCall.IsEditing = true;
            }
        }
    }
}
