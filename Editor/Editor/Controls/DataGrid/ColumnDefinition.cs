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

        public string TemplateName { get; set; }

        public string EditingTemplateName { get; set; }

        public static List<ColumnDefinition> SetCollapsedColumns()
        {
            return new List<ColumnDefinition>()
            {
                new ColumnDefinition()
                {
                    TemplateName = "RefreshBallTemplate",
                },
                new ColumnDefinition()
                {
                    TemplateName = "IsFlaggedTemplate",
                    DataContextPath = "IsFlagged",
                },
                new ColumnDefinition()
                {
                    TemplateName = "IsCanceledTemplate",
                    EditingTemplateName = "IsCanceledTemplate",
                    DataContextPath = "IsCanceled",
                },
            };
        }

        public static List<ColumnDefinition> SetExpanedColumns()
        {
            var columns = SetCollapsedColumns();

            columns.AddRange(new List<ColumnDefinition>()
            {
                new ColumnDefinition()
                {

                },
                new ColumnDefinition()
                {

                },
            });

            return columns;
        }
    }
}
