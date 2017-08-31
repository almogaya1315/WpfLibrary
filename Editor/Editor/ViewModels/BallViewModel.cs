using Editor.Controls.DataGrid;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.ViewModels
{
    public class BallViewModel : ViewModelBase
    {
        public CellDefinition Code { get; set; }

        public CellDefinition IsFlagged { get; set; }

        public CellDefinition IsCanceled { get; set; }

        public CellDefinition Bowler { get; set; }

        public CellDefinition Batsman { get; set; }

        public CellDefinition NonStriker { get; set; }

        public CellDefinition Runs { get; set; }

        public CellDefinition RunType1 { get; set; }

        public CellDefinition RunType2 { get; set; }

        public CellDefinition RunType3 { get; set; }

        public CellDefinition Wicket { get; set; }

        public CellDefinition WicketTypeId { get; set; }
    }
}
