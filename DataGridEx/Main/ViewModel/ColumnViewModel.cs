using Main.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ViewModel
{
    public class ColumnViewModel
    {
        public string Header { get; set; }

        public string DataTemplate { get; set; }

        public string EditingDataTemplate{ get; set; }

        public string DataContext { get; set; }

        public static List<ColumnViewModel> SetColumns()
        {
            var columns = new List<ColumnViewModel>()
            {
                new ColumnViewModel()
                {
                    Header = "Is adult?",
                    DataContext = "IsAdult",
                    DataTemplate = Templates.BoolColumnTemplate.ToString(),
                    EditingDataTemplate = Templates.BoolColumnTemplate.ToString()
                },
                new ColumnViewModel()
                {
                    Header = "Id",
                    DataContext = "Id",
                    DataTemplate = Templates.TextColumnTemplate.ToString(),
                    EditingDataTemplate = Templates.EditingTextColumnTemplate.ToString()
                },
                new ColumnViewModel()
                {
                    Header = "Name",
                    DataContext = "Name",
                    DataTemplate = Templates.TextColumnTemplate.ToString(),
                    EditingDataTemplate = Templates.EditingTextColumnTemplate.ToString()
                },
                new ColumnViewModel()
                {
                    Header = "Relatives",
                    DataContext = "Relatives",
                    DataTemplate = Templates.ListColumnTemplate.ToString(),
                    EditingDataTemplate = Templates.ListColumnTemplate.ToString()
                }
            };

            return columns;
        }
    }
}
