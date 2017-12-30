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

        private void OnAnyGotFocus(object sender, EventArgs e)
        {
            foreach (var item in Items)
            {

                var row = XamlHelper.FindParent<DataGridRow>(item);
            }
        }
    }
}
