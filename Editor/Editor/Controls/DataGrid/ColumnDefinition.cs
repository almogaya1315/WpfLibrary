using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Controls.DataGrid
{
    public class ColumnDefinition
    {
        public string Header { get; set; }

        public string DataContextPath { get; set; }

        public string DataTemplateName { get; set; }

        public string EditingDataTemplateName { get; set; }
    }
}
