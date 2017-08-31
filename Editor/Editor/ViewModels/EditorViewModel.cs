using Editor.Controls.DataGrid;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.ViewModels
{
    public class EditorViewModel : ViewModelBase
    {
        public List<ColumnDefinition> Columns { get; set; }
        private List<ColumnDefinition> _expandedColumns { get; set; }
        private List<ColumnDefinition> _collapsedColumns { get; set; }

        public List<BallViewModel> Balls { get; set; }

        public EditorViewModel()
        {
                           
        }
    }
}
