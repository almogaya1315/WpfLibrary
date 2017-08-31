using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            //AddHandler.Focus
        }
    }
}
